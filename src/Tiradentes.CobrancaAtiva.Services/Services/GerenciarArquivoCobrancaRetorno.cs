using System;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class GerenciarArquivoCobrancaRetorno : IGerenciarArquivoCobrancaRetorno
    {
        public GerenciarArquivoCobrancaRetorno()
        {

        }

        private void ProcessarArquivo()
        {
            //inserir_erro_layout -> Apenas controle de processamento?

            //Validar objeto vazio

            //Validar Tamanho do arquivo?

            //Validar se existe para cada parcela_acordo pelo menos uma parcela_titulo e pelo menos um pagamento
            /*
              inserir_erro_layout( v_dat_hora, 'Não existe parcela de títulos para o acordo: ' ||
                                           v_seq || ' na linha: '|| v_num_linha  );

              inserir_erro_layout( v_dat_hora, 'Não existe pagamento da primeira parcela para o acordo: ' ||
                                           v_seq || ' na linha: '|| v_num_linha  );
             */
        }


        public void Gerenciar()
        {
            throw new NotImplementedException();
        }
    }
}
