function validateForm() {
    let isValid = true;

    const gameName = document.getElementById("gameName").value.trim();
    const category = document.getElementById("selectedCategories").value.trim();
    const gamePrice = document.getElementById("gamePrice").value.trim();
    const gameDescription = document.getElementById("gameDescription").value.trim();
    const gamePicture = document.getElementById("gamePicture").files.length;
    const gameConfiguration = document.getElementById("gameConfiguration").value.trim();
    const gameDownloadFile = document.getElementById("gameDownloadFile").files.length;

    const namebox = document.getElementById("namebox");
    const catebox = document.getElementById("catebox");
    const pricebox = document.getElementById("pricebox");
    const desbox = document.getElementById("desbox");
    const picturebox = document.getElementById("picturebox");
    const conbox = document.getElementById("conbox");
    const downbox = document.getElementById("downbox");

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
    if (gamePicture === 0) {  // Kiểm tra files.length có lớn hơn 0 không
        showError(picturebox);
        isValid = false;
    }
    if (gameConfiguration === "") {
        showError(conbox);
        isValid = false;
    }
    if (gameDownloadFile === 0) {  // Kiểm tra files.length có lớn hơn 0 không
        showError(downbox);
        isValid = false;
    }

    return isValid;
}
