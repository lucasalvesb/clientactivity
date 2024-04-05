CREATE PROCEDURE FI_SP_AltBeneficiario
    @IdBeneficiario BIGINT,
    @Nome           VARCHAR(50),
    @CPF            VARCHAR(11),
    @IdCliente      BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM BENEFICIARIOS WHERE ID = @IdBeneficiario AND IDCLIENTE = @IdCliente)
    BEGIN
        UPDATE BENEFICIARIOS 
        SET 
            NOME = @Nome,
            CPF = @CPF
        WHERE ID = @IdBeneficiario AND IDCLIENTE = @IdCliente

        SELECT 1 AS 'Success'
    END
    ELSE
    BEGIN
        SELECT -1 AS 'Error'
    END
END