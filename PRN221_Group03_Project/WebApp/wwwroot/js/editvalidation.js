function validateForm() {
    let isValid = true;

    const gameName = document.getElementById("gameName").value.trim();
    const category = document.getElementById("selectedCategories").value.trim();
    const gamePrice = document.getElementById("gamePrice").value.trim();
    const gameDescription = document.getElementById("gameDescription").value.trim();
    const gameConfiguration = document.getElementById("gameConfiguration").value.trim();

    const namebox = document.getElementById("namebox");
    const catebox = document.getElementById("catebox");
    const pricebox = document.getElementById("pricebox");
    const desbox = document.getElementById("desbox");
    const conbox = document.getElementById("conbox");

    function showError(box) {
        box.style.visibility = "visible";
        box.style.opacity = 1;

        setTimeout(() => {
            box.style.transition = "opacity 0.5s ease";
            box.style.opacity = 0.5;
        }, 4800);

        setTimeout(() => {
            box.style.visibility = "hidden";
        }, 5000);
    }

    if (gameName === "") {
        showError(namebox);
        isValid = false;
    }
    if (category === "") {
        showError(catebox);
        isValid = false;
    }
    if (gamePrice === "") {
        showError(pricebox);
        isValid = false;
    }
    if (gameDescription === "") {
        showError(desbox);
        isValid = false;
    }
    if (gameConfiguration === "") {
        showError(conbox);
        isValid = false;
    }

    return isValid;
}
