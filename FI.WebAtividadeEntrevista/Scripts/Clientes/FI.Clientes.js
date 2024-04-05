$(document).ready(function () {
    $('#formCadastro').submit(function (e) {
        e.preventDefault();

        var formData = {
            "NOME": $(this).find("#Nome").val(),
            "CPF": $(this).find("#CPF").val(),
            "CEP": $(this).find("#CEP").val(),
            "Email": $(this).find("#Email").val(),
            "Sobrenome": $(this).find("#Sobrenome").val(),
            "Nacionalidade": $(this).find("#Nacionalidade").val(),
            "Estado": $(this).find("#Estado").val(),
            "Cidade": $(this).find("#Cidade").val(),
            "Logradouro": $(this).find("#Logradouro").val(),
            "Telefone": $(this).find("#Telefone").val()
        };
        console.log("Form Data:", formData);

        $.ajax({
            url: urlPost,
            method: "POST",
            data: formData,
            error: function (xhr) {
                if (xhr.status == 400) {
                    ModalDialog("Erro", "CPF já cadastrado");
                } else if (xhr.status == 500) {
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                }
            },
            success: function (response) {
                if (response.success) {
                    ModalDialog("Sucesso!", "Cadastro efetuado com sucesso");
                    $("#formCadastro")[0].reset();
                } else {
                    ModalDialog("Atenção!", response.message);
                }
            }
        });
    });
});


function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

const cpfInput = document.getElementById('CPF');

cpfInput.addEventListener('input', function (event) {
    let value = event.target.value.replace(/\D/g, ''); 

    if (value.length > 11) {
        value = value.slice(0, 11); 
    }


    let formattedCPF = '';
    for (let i = 0; i < value.length; i++) {
        if (i === 3 || i === 6) {
            formattedCPF += '.';
        } else if (i === 9) {
            formattedCPF += '-';
        }
        formattedCPF += value[i];
    }

    event.target.value = formattedCPF;
});

const cpfInputModal = document.getElementById('cpfModal');

cpfInputModal.addEventListener('input', function (event) {
    let value = event.target.value.replace(/\D/g, '');

    if (value.length > 11) {
        value = value.slice(0, 11);
    }


    let formattedCPF = '';
    for (let i = 0; i < value.length; i++) {
        if (i === 3 || i === 6) {
            formattedCPF += '.';
        } else if (i === 9) {
            formattedCPF += '-';
        }
        formattedCPF += value[i];
    }

    event.target.value = formattedCPF;
});