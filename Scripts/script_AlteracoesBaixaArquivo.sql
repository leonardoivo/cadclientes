-- INICIO - scf.Parcelas_Titulos
ALTER TABLE scf.Parcelas_Titulos
    ADD (
    
        CNPJ_EMPRESA_COBRANCA VARCHAR(14),
        SISTEMA CHAR(1),
        TIPO_INADIMPLENCIA CHAR(1),
        PERIODO_CHEQUE_DEVOLVIDO NUMBER(5,0)
    )
    
COMMENT ON COLUMN scf.Parcelas_Titulos.SISTEMA 
   IS 'S - Graduação Presencial
       E - Ensino a Distância
       P - Pós-Graduação Lato Sensu
       I  - Pós-Graduação Stricto Sensu
       X - Atividades de Extensão
        ';
COMMENT ON COLUMN scf.Parcelas_Titulos.TIPO_INADIMPLENCIA
    IS 'P - Parcela do curso do aluno
        T - Parcela de Negociação Título Avulso
        R - Parcela de Renegociação de Quebra de Acordo
        C - Parcela de Devolução de Cheque
    ';
-- FIM - scf.Parcelas_Titulos

-- INICIO scf.ACORDOS_COBRANCAS
ALTER TABLE scf.ACORDOS_COBRANCAS
    ADD (
        CNPJ_EMPRESA_COBRANCA VARCHAR(14),
        CPF VARCHAR(11),
        SISTEMA CHAR(1),
        TIPO_INADIMPLENCIA CHAR(1)
    )
    
COMMENT ON COLUMN scf.ACORDOS_COBRANCAS.SISTEMA 
IS 'S - Graduação Presencial
   E - Ensino a Distância
   P - Pós-Graduação Lato Sensu
   I  - Pós-Graduação Stricto Sensu
   X - Atividades de Extensão
    ';
    
COMMENT ON COLUMN scf.ACORDOS_COBRANCAS.TIPO_INADIMPLENCIA
IS 'P - Parcela do curso do aluno
    T - Parcela de Negociação Título Avulso
    R - Parcela de Renegociação de Quebra de Acordo
    C - Parcela de Devolução de Cheque
';
-- FIM scf.ACORDOS_COBRANCAS

-- INICIO PARCELAS_ACORDO
ALTER TABLE scf.PARCELAS_ACORDO
    ADD (
        CNPJ_EMPRESA_COBRANCA VARCHAR(14),
        SISTEMA CHAR(1),
        TIPO_INADIMPLENCIA CHAR(1)
    )
    
COMMENT ON COLUMN scf.PARCELAS_ACORDO.SISTEMA 
IS 'S - Graduação Presencial
   E - Ensino a Distância
   P - Pós-Graduação Lato Sensu
   I  - Pós-Graduação Stricto Sensu
   X - Atividades de Extensão
    ';
    
COMMENT ON COLUMN scf.PARCELAS_ACORDO.TIPO_INADIMPLENCIA
IS 'P - Parcela do curso do aluno
    T - Parcela de Negociação Título Avulso
    R - Parcela de Renegociação de Quebra de Acordo
    C - Parcela de Devolução de Cheque
';
-- FIM PARCELAS_ACORDO

-- INICIO ITENS_BAIXAS_TIPO3
ALTER TABLE scf.ITENS_BAIXAS_TIPO3
    ADD (
        CNPJ_EMPRESA_COBRANCA VARCHAR(14),
        STA_ALU CHAR(1),
        SISTEMA CHAR(1),
        TIPO_INADIMPLENCIA CHAR(1),
        TIPO_PGTO CHAR(1)
    )
COMMENT ON COLUMN scf.ITENS_BAIXAS_TIPO3.STA_ALU
IS 'M - Matriculado
    F - Formado
    A - Abandonado
    C - Cancelado
    T - Trancado
    J - Jubilado
    D - Desistente SPGL (Pós-Graduação Lato Sensu)
    ';

COMMENT ON COLUMN scf.ITENS_BAIXAS_TIPO3.TIPO_PGTO
IS 'B - Banco
    C - Cartão de Crédito
    ';
    
COMMENT ON COLUMN scf.ITENS_BAIXAS_TIPO3.SISTEMA 
IS 'S - Graduação Presencial
   E - Ensino a Distância
   P - Pós-Graduação Lato Sensu
   I  - Pós-Graduação Stricto Sensu
   X - Atividades de Extensão
    ';
    
COMMENT ON COLUMN scf.ITENS_BAIXAS_TIPO3.TIPO_INADIMPLENCIA
IS 'P - Parcela do curso do aluno
    T - Parcela de Negociação Título Avulso
    R - Parcela de Renegociação de Quebra de Acordo
    C - Parcela de Devolução de Cheque
';
-- FIM ITENS_BAIXAS_TIPO3

-- INICIO scf.ITENS_BAIXAS_TIPO1
ALTER TABLE scf.ITENS_BAIXAS_TIPO1
    ADD (
        CNPJ_EMPRESA_COBRANCA VARCHAR(14),
        STA_ALU CHAR(1),
        SISTEMA CHAR(1),
        TIPO_INADIMPLENCIA CHAR(1)
    )
COMMENT ON COLUMN scf.ITENS_BAIXAS_TIPO1.STA_ALU
IS 'M - Matriculado
    F - Formado
    A - Abandonado
    C - Cancelado
    T - Trancado
    J - Jubilado
    D - Desistente SPGL (Pós-Graduação Lato Sensu)
    ';
  
COMMENT ON COLUMN scf.ITENS_BAIXAS_TIPO1.SISTEMA 
IS 'S - Graduação Presencial
   E - Ensino a Distância
   P - Pós-Graduação Lato Sensu
   I  - Pós-Graduação Stricto Sensu
   X - Atividades de Extensão
    ';
    
COMMENT ON COLUMN scf.ITENS_BAIXAS_TIPO1.TIPO_INADIMPLENCIA
IS 'P - Parcela do curso do aluno
    T - Parcela de Negociação Título Avulso
    R - Parcela de Renegociação de Quebra de Acordo
    C - Parcela de Devolução de Cheque
';
-- FIM scf.ITENS_BAIXAS_TIPO1

ALTER TABLE scf.ITENS_BAIXAS_TIPO2
    ADD (
        CNPJ_EMPRESA_COBRANCA VARCHAR(14),
        STA_ALU CHAR(1),
        SISTEMA CHAR(1),
        TIPO_INADIMPLENCIA CHAR(1),
        PERIODO_CHEQUE_DEVOLVIDO NUMBER(5,0)
    )
    
COMMENT ON COLUMN scf.ITENS_BAIXAS_TIPO2.STA_ALU
IS 'M - Matriculado
    F - Formado
    A - Abandonado
    C - Cancelado
    T - Trancado
    J - Jubilado
    D - Desistente SPGL (Pós-Graduação Lato Sensu)
    ';
  
COMMENT ON COLUMN scf.ITENS_BAIXAS_TIPO2.SISTEMA 
IS 'S - Graduação Presencial
   E - Ensino a Distância
   P - Pós-Graduação Lato Sensu
   I  - Pós-Graduação Stricto Sensu
   X - Atividades de Extensão
    ';
    
COMMENT ON COLUMN scf.ITENS_BAIXAS_TIPO2.TIPO_INADIMPLENCIA
IS 'P - Parcela do curso do aluno
    T - Parcela de Negociação Título Avulso
    R - Parcela de Renegociação de Quebra de Acordo
    C - Parcela de Devolução de Cheque
'; 