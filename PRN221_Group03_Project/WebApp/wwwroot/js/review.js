function validateForm() {

    let isValid = true;

    const rates = document.getElementsByName("rate");
    const cmt = document.getElementById("comment").value.trim();

    const rateTooltip = document.querySelector("#myrate");
    const cmtTooltip = document.querySelector("#mycomment");

    let rateSelected = false;

    for (const input of rates) {
        if (input.checked) {
            rateSelected = true;
            break;
        }
    }

    if (!rateSelected) {
        rateTooltip.style.visibility = "visible";
        rateTooltip.style.opacity = 1; 
        isValid = false;

        setTimeout(() => {
            rateTooltip.style.transition = "opacity 0.5s ease"; 
            rateTooltip.style.opacity = 0.5; 
        }, 4800);

        setTimeout(() => {
            rateTooltip.style.visibility = "hidden";
        }, 5000);
    } else {
        rateTooltip.style.visibility = "hidden";
    }

    if (cmt === "") {
        cmtTooltip.style.visibility = "visible";
        cmtTooltip.style.opacity = 1; 
        isValid = false;

        setTimeout(() => {
            cmtTooltip.style.transition = "opacity 0.5s ease"; 
            cmtTooltip.style.opacity = 0.5; 
        }, 4800);

        setTimeout(() => {
            cmtTooltip.style.visibility = "hidden";
        }, 5000);
    } else {
        cmtTooltip.style.visibility = "hidden";
    }

    return isValid;
}