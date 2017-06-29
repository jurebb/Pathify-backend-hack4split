$(document).ready(function() {
   let $menu_items = $('.navbar-inverse .navbar-nav>');
    
    $menu_items.on("click", e => {
        $menu_items.each((index,element) => {
            $(element).removeClass('activated');
        });
       
        let $selected_item = $(e.currentTarget);
        $selected_item.addClass('activated');
    });
    
    /*Add class hovered to hovered item
    $menu_items.on("mouseenter", e => {   
        let $selected_item = $(e.currentTarget);
        $selected_item.addClass('hovered');
    });
    
    $menu_items.on("mouseleave", e => {
         $menu_items.each((index,element) => {
            $(element).removeClass('hovered');
        });
    });*/
});