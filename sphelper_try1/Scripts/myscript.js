$(".remove-crn").on("click", function () {
    $(this).prev('input:hidden').val('true');
    $(this).closest('tr').remove();
    //return false;
})

$(".generatecrn-submit-btn").on("click"), function () {
    document.forms[0].submit();
    //return false;
}

const menubutton = document.getElementById("toggle")
const showcase = document.querySelector(".showcase")

menubutton.addEventListener("click", function () {
    showcase.classList.toggle('isactive')
    menubutton.classList.toggle('isactive')
})