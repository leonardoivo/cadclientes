using AutoMapper;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ParametroEnvioService : BaseService, IParametroEnvioService
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMQConfig _rabbitMQConfig;
        private readonly IParametroEnvioRepository _repositorio;
        private readonly IGeracaoCobrancasRepository _geracaoCobrancaRepositorio;
        protected readonly IMapper _map;

        public ParametroEnvioService(
            IParametroEnvioRepository repositorio, 
            IGeracaoCobrancasRepository geracaoCobrancaRepositorio,
            IMapper map,
            IOptions<RabbitMQConfig> rabbitMQConfig
        )
        { 
            _map = map;
            _repositorio = repositorio;
            _geracaoCobrancaRepositorio = geracaoCobrancaRepositorio;
            _rabbitMQConfig = rabbitMQConfig.Value;

            _factory = new ConnectionFactory
            {
                HostName = _rabbitMQConfig.HostName,
                VirtualHost = _rabbitMQConfig.VirtualHost,
                UserName = _rabbitMQConfig.UserName,
                Password = _rabbitMQConfig.Password
            };
        }

        public async Task<ViewModelPaginada<BuscaParametroEnvioViewModel>> Buscar(ConsultaParametroEnvioQueryParam queryParam)
        {
            var regraQueryParam = _map.Map<ParametroEnvioQueryParam>(queryParam);

            var list = await _repositorio.Buscar(regraQueryParam);

            return _map.Map<ViewModelPaginada<BuscaParametroEnvioViewModel>>(list);
        }

        public async Task<BuscaParametroEnvioViewModel> BuscarPorId(int id)
        {
            var list = await _repositorio.BuscarPorIdComRelacionamentos(id);

            return _map.Map<BuscaParametroEnvioViewModel>(list);
        }

        public async Task EnviarParametroParaConsumer(int id)
        {
            var parametroEnvio = await _repositorio.BuscarPorIdComRelacionamentos(id);

            var geracaoArquivo = new GeracaoCobrancasModel() {
                CnpjEmpresaCobranca = parametroEnvio.EmpresaParceira.CNPJ,
                DataGeracao = "07/12/2021",
                DataInicio = "01/01/2020",
                DataFim = "01/01/2020",
                DataHoraEnvio = "01/01/2020",
                Sistema = "S",
                Username = "APP_COBRANCA",
                TipoInadimplencia = "T"
            };

            await _geracaoCobrancaRepositorio.Criar(geracaoArquivo);

            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: _rabbitMQConfig.Queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var viewModels = new ParametroEnvioFilaViewModel();
                    viewModels.Instituicao = parametroEnvio.Instituicao.Id;
                    viewModels.Modalidade = parametroEnvio.Modalidade.CodigoMagister;
                    viewModels.InadimplenciaInicial = parametroEnvio.InadimplenciaInicial;
                    viewModels.InadimplenciaFinal = parametroEnvio.InadimplenciaFinal;
                    viewModels.EmpresaParceira = parametroEnvio.EmpresaParceira.Id;
                    viewModels.ValidadeInicial = parametroEnvio.ValidadeInicial;
                    viewModels.ValidadeFinal = parametroEnvio.ValidadeFinal;
                    viewModels.Cursos = parametroEnvio.Cursos.Select(x => x.CodigoMagister.ToString()).ToArray();
                    viewModels.SituacoesAluno = parametroEnvio.SituacoesAlunos.Select(x => x.CodigoMagister.ToString()).ToArray();
                    viewModels.TiposTitulos = parametroEnvio.TiposTitulos.Select(x => x.CodigoMagister.ToString()).ToArray();
                    viewModels.TitulosAvulsos = parametroEnvio.TitulosAvulsos.Select(x => x.CodigoGT.ToString()).ToArray();

                    var stringfiedMessage = JsonSerializer.Serialize(viewModels);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: _rabbitMQConfig.Queue,
                        basicProperties: null,
                        body: bytesMessage);
                }
            }
        }

        public async Task<ParametroEnvioViewModel> Criar(CriarParametroEnvioViewModel viewModel)
        {
            //Validate(new CriarRegraNegociacaoValidation(), viewModel);

            var model = _map.Map<ParametroEnvioModel>(viewModel);

            await _repositorio.Criar(model);

            return _map.Map<ParametroEnvioViewModel>(model);
        }

        public async Task<ParametroEnvioViewModel> Alterar(AlterarParametroEnvioViewModel viewModel)
        {
            var modelBanco = await _repositorio.BuscarPorId(viewModel.Id);

            if (modelBanco == null)
            {
                EntidadeNaoEncontrada("Parametro envio não cadastrado.");
                return null;
            }

            var model = _map.Map<ParametroEnvioModel>(viewModel);

            model.SetParametroEnvioCurso(modelBanco.ParametroEnvioCurso);
            model.SetParametroEnvioSituacaoAluno(modelBanco.ParametroEnvioSituacaoAluno);
            model.SetParametroEnvioTituloAvulso(modelBanco.ParametroEnvioTituloAvulso);
            model.SetParametroEnvioTipoTitulo(modelBanco.ParametroEnvioTipoTitulo);

            await _repositorio.Alterar(model);

            return _map.Map<ParametroEnvioViewModel>(model);
        }

        public async Task Deletar(int id)
        {
            await _repositorio.Deletar(id);
        }
    }
}
