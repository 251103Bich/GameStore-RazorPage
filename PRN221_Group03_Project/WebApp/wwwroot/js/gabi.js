/*let expanded = false;

// Toggle showing/hiding the checkboxes
function toggleCheckboxes() {
    const checkboxes = document.getElementById("checkboxes");
    checkboxes.style.display = expanded ? "none" : "block";
    expanded = !expanded;
}

// Get all checkboxes and show selected options in a div
document.querySelectorAll('.checkboxes input[type="checkbox"]').forEach(checkbox => {
    checkbox.addEventListener('change', function () {
        const selected = [];
        const selectedValues = []; // Mảng để lưu giá trị đã chọn
        document.querySelectorAll('.checkboxes input[type="checkbox"]:checked').forEach(checkedBox => {
            selected.push(checkedBox.parentElement.textContent.trim());
            selectedValues.push(checkedBox.value); // Lưu giá trị vào mảng
        });

        // Show selected options in a div
        document.getElementById("selectedOptionsDiv").textContent = selected.join(', ');

        // Cập nhật giá trị của trường ẩn
        document.getElementById("selectedCategories").value = selectedValues.join(','); // Chuyển đổi thành chuỗi
    });
});
*/

let expanded = false;

// Toggle showing/hiding the checkboxes
function toggleCheckboxes() {
    const checkboxes = document.getElementById("checkboxes");
    checkboxes.style.display = expanded ? "none" : "block";
    expanded = !expanded;
}

// Get all checkboxes and show selected options in a div
document.querySelectorAll('.checkboxes input[type="checkbox"]').forEach(checkbox => {
    checkbox.addEventListener('change', function () {
        const selected = [];
        const selectedValues = []; // Mảng để lưu giá trị đã chọn
        document.querySelectorAll('.checkboxes input[type="checkbox"]:checked').forEach(checkedBox => {
            selected.push(checkedBox.parentElement.textContent.trim());
            selectedValues.push(checkedBox.value); // Lưu giá trị vào mảng
        });

        // Lấy phần tử selectedOptionsDiv
        const selectedOptionsDiv = document.getElementById("selectedOptionsUl");
        selectedOptionsDiv.innerHTML = ''; // Xóa nội dung cũ

        // Tạo các thẻ li với class tag và thêm vào selectedOptionsDiv
        selected.forEach(option => {
            const li = document.createElement('li');
            li.classList.add('tag');
            li.textContent = option;
            selectedOptionsDiv.appendChild(li);
        });

        // Cập nhật giá trị của trường ẩn
        document.getElementById("selectedCategories").value = selectedValues.join(','); // Chuyển đổi thành chuỗi
    });
});
