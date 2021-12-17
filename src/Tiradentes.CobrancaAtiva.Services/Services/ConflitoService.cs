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
using Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ConflitoService : BaseService, IConflitoService
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMQConfig _rabbitMQConfig;
        private readonly IConflitoRepository _repositorio;
        protected readonly IMapper _map;

        public ConflitoService(IConflitoRepository repositorio,
            IMapper map,
            IOptions<RabbitMQConfig> rabbitMQConfig)
        {
            _map = map;
            _repositorio = repositorio;
            _rabbitMQConfig = rabbitMQConfig.Value;

            _factory = new ConnectionFactory
            {
                HostName = _rabbitMQConfig.HostName,
                VirtualHost = _rabbitMQConfig.VirtualHost,
                UserName = _rabbitMQConfig.UserName,
                Password = _rabbitMQConfig.Password
            };
        }

        public async Task<ViewModelPaginada<ConflitoViewModel>> Buscar(ConflitoQueryParam queryParam)
        {
            var regraQueryParam = _map.Map<ConflitoQueryParam>(queryParam);

            var list = await _repositorio.Buscar(regraQueryParam);

            return _map.Map<ViewModelPaginada<ConflitoViewModel>>(list);
        }

        //public async Task<BuscaConflitoViewModel> BuscarPorId(int id)
        //{
        //    var list = await _repositorio.BuscarPorIdComRelacionamentos(id);

        //    return _map.Map<BuscaConflitoViewModel>(list);
        //}

        //public async Task EnviarParametroParaConsumer(int id)
        //{
        //    var Conflito = await _repositorio.BuscarPorIdComRelacionamentos(id);

        //    using (var connection = _factory.CreateConnection())
        //    {
        //        using (var channel = connection.CreateModel())
        //        {
        //            channel.QueueDeclare(
        //                queue: _rabbitMQConfig.Queue,
        //                durable: false,
        //                exclusive: false,
        //                autoDelete: false,
        //                arguments: null);

        //            var viewModels = new ConflitoFilaViewModel();
        //            viewModels.Instituicao = Conflito.Instituicao.Id;
        //            viewModels.Modalidade = Conflito.Modalidade.CodigoMagister;
        //            viewModels.InadimplenciaInicial = Conflito.InadimplenciaInicial;
        //            viewModels.InadimplenciaFinal = Conflito.InadimplenciaFinal;
        //            viewModels.EmpresaParceira = Conflito.EmpresaParceira.Id;
        //            viewModels.ValidadeInicial = Conflito.ValidadeInicial;
        //            viewModels.ValidadeFinal = Conflito.ValidadeFinal;
        //            viewModels.Cursos = Conflito.Cursos.Select(x => x.CodigoMagister.ToString()).ToArray();
        //            viewModels.SituacoesAluno = Conflito.SituacoesAlunos.Select(x => x.CodigoMagister.ToString()).ToArray();
        //            viewModels.TiposTitulos = Conflito.TiposTitulos.Select(x => x.CodigoMagister.ToString()).ToArray();
        //            viewModels.TitulosAvulsos = Conflito.TitulosAvulsos.Select(x => x.CodigoGT.ToString()).ToArray();

        //            var stringfiedMessage = JsonSerializer.Serialize(viewModels);
        //            var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

        //            channel.BasicPublish(
        //                exchange: "",
        //                routingKey: _rabbitMQConfig.Queue,
        //                basicProperties: null,
        //                body: bytesMessage);
        //        }
        //    }
        //}

        //public async Task<ConflitoViewModel> Criar(CriarConflitoViewModel viewModel)
        //{
        //    //Validate(new CriarRegraNegociacaoValidation(), viewModel);

        //    var model = _map.Map<ConflitoModel>(viewModel);

        //    await _repositorio.Criar(model);

        //    return _map.Map<ConflitoViewModel>(model);
        //}

        //public async Task<ConflitoViewModel> Alterar(AlterarConflitoViewModel viewModel)
        //{
        //    var modelBanco = await _repositorio.BuscarPorId(viewModel.Id);

        //    if (modelBanco == null)
        //    {
        //        EntidadeNaoEncontrada("Parametro envio não cadastrado.");
        //        return null;
        //    }

        //    var model = _map.Map<ConflitoModel>(viewModel);

        //    model.SetConflitoCurso(modelBanco.ConflitoCurso);
        //    model.SetConflitoSituacaoAluno(modelBanco.ConflitoSituacaoAluno);
        //    model.SetConflitoTituloAvulso(modelBanco.ConflitoTituloAvulso);
        //    model.SetConflitoTipoTitulo(modelBanco.ConflitoTipoTitulo);

        //    await _repositorio.Alterar(model);

        //    return _map.Map<ConflitoViewModel>(model);
        //}

        //public async Task Deletar(int id)
        //{
        //    await _repositorio.Deletar(id);
        //}
    }
}
