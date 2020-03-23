$(document).ready(function () {
    var table = $('#example').DataTable(
        {
            "language": {
                "url": "/Scripts/russian.json"
            }
        }

    );
 
    $('#example tbody').on( 'click', 'tr', function () {
        if ( $(this).hasClass('selected') ) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    } );
 
    $('#button').click( function () {
        table.row('.selected').remove().draw( false );
    } );
} ); 