
using MongoDB.Bson;
using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca
{
    public class RespostaViewModel
    {
        public int TipoRegistro { get; set; }
        public Int64 CnpjEmpresaCobranca { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
        public string ChaveInadimplencia { get; set; }
        public int CodigoInstituicaoEnsino { get; set; }
        public int? Curso { get; set; }
        public Int64 CPF { get; set; }
        public string NomeAluno { get; set; }
        public Int64 Matricula { get; set; }
        public Int64 NumeroAcordo { get; set; }
        public int Parcela { get; set; }

        /*
            Valores possíveis para o campo “PERIODO” por modalidade/inadimplência:
            Graduação Presencial (SCA)
            PERIODO    = ANO||’#’||SEMESTRE	(em SCA.PGTO_ALUNOS)

            Ensino a Distância (EAD)
            PERIODO    = PERIODO (em PROFOPE.PGTO_ALUNOS)

            Pós-Graduação Lato Sensu (SPGL)
            PERIODO    = ANO||’#’||SEMESTRE	(em SPGL.PGTO_ALUNOS)

            Pós-Graduação Stricto Sensu (SIP)
            PERIODO    = RENOVACAO (em SCF.V_PGTO_TITULOS)

            Atividades de Extensão (EXTENSAO)
            PERIODO    = COD_ATV ||’#’|| NUM_EVT (em EXTENSAO.PAGAMENTOS)

            Negociação de Parcelas (Títulos Avulsos)
            PERIODO    = IDT_TITULO_AVU (em SCF.SAP_TITULOS_AVULSOS)

            Cheques Devolvidos
            PERIODO    = COD_BANCO||’#’||COD_AGENCIA||’#’||NUM_CONTA||’#’||NUM_CHEQUE 	(em SCF.CHEQUES)

            Renegociação de Quebra de Acordo (referente à empresa de cobrança)
            PERIODO    = CNPJ_EMPRESA||’#’||NUM_ACORDO (em SCF.ACORDOS_COBRANCAS)
         */
        public string Periodo { get; set; }
        public decimal IdTitulo { get; set; } //idt_tipo_titulo (TIPO TÍTULO AVULSO)
        public int IdAluno { get; set; } //idt_alu (IDENTIFICADOR DO ALUNO)
        public decimal IdPessoa { get; set; } //idt_ddp (IDENTIFICADOR DA PESSOA)


        public int CodigoAtividade { get; set; }
        public int NumeroEvt { get; set; }
        public int CodigoBanco { get; set; }
        public int CodigoAgencia { get; set; }
        public int NumeroConta { get; set; }
        public Int64 NumeroCheque { get; set; }


        //public RespostaRegistroTipo1ViewModel RespostaRegistroTipo1 { get; set; }
        public decimal JurosParcela { get; set; }
        public decimal MultaParcela { get; set; }
        public decimal ValorTotalParcela { get; set; }
        public DateTime DataFechamentoAcordo { get; set; }
        public int TotalParcelasAcordo { get; set; }
        public DateTime DataVencimentoParcela { get; set; }
        public decimal ValorParcela { get; set; }


        //public RespostaRegistroTipo2ViewModel RespostaRegistroTipo2 { get; set; }
        //public DateTime DataVencimentoParcela { get; set; }
        //public decimal ValorParcela { get; set; }
        public decimal SaldoDevedorTotal { get; set; }
        public string Produto { get; set; }
        public string DescricaoProduto { get; set; }
        //public string Fase { get; set; }
        public string CodigoControleCliente { get; set; }


        //public RespostaRegistroTipo3ViewModel RespostaRegistroTipo3 { get; set; }
        public string NossoNumero { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataBaixa { get; set; }
        public decimal ValorPago { get; set; }
        public string TipoPagamento { get; set; }
        public bool Integrado { get; set; }
        public int Erro { get; set; } 

        public decimal ObterPeriodo()
        {
            if (this.TipoInadimplencia.Equals("C") || this.TipoInadimplencia.Equals("X"))
                return 1;

            return Convert.ToDecimal(Periodo);
        }
        public string ObterPeriodoOutros()
        {
            if (this.TipoInadimplencia.Equals("C") || this.TipoInadimplencia.Equals("X"))
                return Periodo;

            return "1";
        }
    }
}
