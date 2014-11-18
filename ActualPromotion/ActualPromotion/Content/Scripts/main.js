//
// Functions to be executed on document.ready
// Code is separated as much as possible to avoid clutter
// --------------------------------------------------

$( document ).ready( function() {

    // Handle form submission
    //$( '.contact-form' ).on( 'submit.mist', contactFormHandler );
    $( '.subscription-form' ).on( 'submit.mist', subscriptionFormHandler );

    // Start all carousels with a reasonable interval
    $( '.carousel' ).carousel( {
        interval: 10000
    } );

    // A little snippet that adds bottom-border to the navbar, but only on big enough screens
    navbarBorder();


    // Show the contact form
    $( 'body' ).on( 'click.mist', '.contact-toggle', function( event ) {
        event.preventDefault();
        $( 'body' ).toggleClass( 'open' );
    } );

    // Collapse mobile menu when clicked
    /*$( 'body' ).on( 'click.mist', '.navbar-nav', function() {
        $( this ).closest( '.navbar-collapse' ).collapse( 'hide' );
    } );*/
	
} );


//
// Functions that require images to load
// --------------------------------------------------

$( window ).load( function() {

    // Scrollspy and smoothscroll combined
    $( '.navbar-nav' ).smartScroll( {
        offset: 75,
        activeParent: 'li'
    } );

} );


//
// Handle contact form submission
// --------------------------------------------------

function contactFormHandler(event) {
    // Prevent default form submission
    event.preventDefault();
    // Cache form for later use
    var $form = $( this ),
        $submit = $form.find('[type="submit"]');

    var isExit = false;
    $(this).find("input.required").each(function (index, element) {
        if ($(element).val() == "") {
            $(element).addClass("warning");
            isExit = true;
        }
        else {
            $(element).removeClass("warning");
        }
    });

    var pattern = /^([a-z0-9_\.-])+@[a-z0-9-]+\.([a-z]{2,4}\.)?[a-z]{2,4}$/i;
    if (!pattern.test($("input[name='mail']").val())) {
        $(this).find("input[name='mail']").addClass("warning");
        isExit = true;
    }

    if (isExit) {
        return false;
    }
    formData = new FormData($(this)[0]);

    // Disable the submit button and change it's text to prevent multiple submissions and inform the user that something is happening
    $submit.button( 'loading' );
	$('.help-block').remove();
    // Send ajax request
	console.log("5555");
    $.ajax( {
        url: 'includes/functions.php',
        type: 'post',
        data: formData,
        cache: false,
        processData: false,
        contentType: false,
        //data: $( this ).serialize() + '&action=contact',
        success: function (msg) {
            $submit.html(msg.message);
			/*if (msg.status == undefined) {
				// This needs heavy optimization
				var helperClass = 'help-block',
					$helperElement = $( '<p class="' + helperClass + '">' + msg.message + '</p>' ),
					$form_control = $form.find( '[name="' + msg.field + '"]' ),
					$form_control_parent = $form_control.closest( '.form-group' );

				$submit.button( 'reset' );
				console.log("dfsdfsd");

				$form_control_parent.removeClass( function( index, css ) {
					return ( css.match( /\bhas-\S+/g ) || [] ).join( ' ' );
				} ).addClass( 'has-' + msg.status );

				if ( $form_control_parent.find( '.' + helperClass ).length ) {
					$form_control_parent.find( '.' + helperClass ).text( msg.message );
				}
				else {
					if ( $form_control.parent( '.input-group' ).length ) {
						$helperElement.insertAfter( $form_control.parent( '.input-group' ) );
					}
					else {
						$helperElement.insertAfter( $form_control );
					}
				}
			}
			else {
				$submit.html(msg.status);
				setTimeout(function() {
					$submit.button( 'reset' );
				}, 5000);
			}*/
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Handle errors here
            console.log('ERRORS: ' + textStatus);
            // STOP LOADING SPINNER
        }
    } );
}


//
// Handle subscription form submission
// --------------------------------------------------

function subscriptionFormHandler( event ) {
    alert("6666");
    // Prevent default form submission
    event.preventDefault();

    // Cache form for later use
    var $form = $( this ),
        $submit = $form.find( '[type="submit"]' );

    // Disable the submit button and change it's text to prevent multiple submissions and inform the user that something is happening
    $submit.button( 'loading' );

    // Send ajax request
    $.ajax( {
        url: 'includes/functions.php',
        type: 'post',
        dataType: 'json',
        data: $( this ).serialize() + '&action=newsletter',
        success: function( msg ) {
            // This needs heavy optimization
            var helperClass = 'help-block',
                $helperElement = $( '<p class="' + helperClass + '">' + msg.message + '</p>' ),
                $form_control = $form.find( '[name="' + msg.field + '"]' ),
                $form_group = $form_control.closest( '.form-group' );

            $submit.button('reset');
            

            $form_group.removeClass( function( index, css ) {
                return ( css.match( /\bhas-\S+/g ) || [] ).join( ' ' );
            } ).addClass( 'has-' + msg.status );

            if ( $form_group.find( '.' + helperClass ).length ) {
                $form_group.find( '.' + helperClass ).text( msg.message );
            }
            else {
                if ( $form_control.parent( '.input-group' ).length ) {
                    $helperElement.insertAfter( $form_control.parent( '.input-group' ) );
                }
                else {
                    $helperElement.insertAfter( $form_control );
                }
            }
        },
        error: function() {
            // Comment this out because of IE8
            // console.log('ajax could not be completed');
        }
    } );
}


//
// A little snippet that adds bottom-border to the navbar, but only on big enough screens
// --------------------------------------------------

function navbarBorder() {
    var timer, offset,
        $navbar = $( '.navbar' );

    $( window ).on( 'scroll.mist', function() {
        clearTimeout( timer );
        timer = setTimeout( function() {
            if ( $navbar.css( 'outline-style' ) === 'dotted' ) {
                offset = $navbar.offset().top;
                if ( offset ) {
                    if ( !$navbar.hasClass( 'sticky' ) ) {
                        $navbar.addClass( 'sticky' );
                    }
                }
                else {
                    $navbar.removeClass( 'sticky' );
                }
            }
        }, 100 );
    } );
}
