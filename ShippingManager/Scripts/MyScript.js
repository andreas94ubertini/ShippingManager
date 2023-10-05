
let CheckBox = document.getElementById("IsAzienda")
CheckBox.addEventListener("change", e => {
    if (e.target.checked) {
        document.getElementById("CfCont").classList.add("d-none")
        document.getElementById("PivaCont").classList.remove("d-none")
    } else {
        document.getElementById("CfCont").classList.remove("d-none")
        document.getElementById("PivaCont").classList.add("d-none")
    }


})
