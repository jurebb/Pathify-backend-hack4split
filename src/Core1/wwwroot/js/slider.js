$(document).ready(function() {
    
    let $pictures_array = $('#route-pictures img');
    $($pictures_array[0]).removeClass('hidden');
    $($pictures_array[1]).removeClass('hidden');
    
    let index_right=1;
    let index_left=0;
    
    $('#route-pictures .right').on("click", e => {
        index_right++;
        index_left++;
        
        if(index_right>=$pictures_array.length)
            index_right=0;
        
         if(index_left>=$pictures_array.length)
            index_left=0;
        
        $pictures_array.each((index,element) => {
            $(element).addClass('hidden');
            $($pictures_array[index_left]).removeClass('hidden');
            $($pictures_array[index_right]).removeClass('hidden');
        });
    });
    
    $('#route-pictures .left').on("click", e => {
        index_right--;
        index_left--;
        
        if(index_right<0)
            index_right=$pictures_array.length-1;
         if(index_left<0)
            index_left=$pictures_array.length-1;
        
        $pictures_array.each((index,element) => {
            $(element).addClass('hidden');
            $($pictures_array[index_left]).removeClass('hidden');
            $($pictures_array[index_right]).removeClass('hidden');
        });
    });
});