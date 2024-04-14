
async function deleteSupplement(id) {
    if (confirm("Confirm that you want to delete this supplement.")) {
        
        await fetch('/Supplement/Delete/' + id, {
            method: 'DELETE'
        }).then(res => {
            if (res.ok) {
                const child = document.getElementById(id);
                const parent = document.getElementById("supplements-list");
                parent.removeChild(child);
            } else if (res.status === 404) {
                alert('Item not found.');
            }
        });
    }
}

async function updateSupplement(id) {
    event.preventDefault();

    const form = document.getElementById('update-supplement');
    const formData = new FormData(form);
    let jsonData = {};

    for (let [key, value] of formData.entries()) {
        jsonData[key] = value;
    }

    await fetch('/Supplement/Update/' + id, {
        method: "PUT",
        body: JSON.stringify(jsonData),
        headers: {
            "Content-Type": "application/json"
        }
    }).then(data => {
        window.location.href = '/Supplement/Index';
    });
}

async function showUpdateSupplementView(id) {
    await fetch('/Supplement/Update/' + id, {
        method: 'GET'
    }).then(res => window.location.href = '/Supplement/Update/' + id);
}
















