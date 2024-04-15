async function deleteNews(id) {
    if (confirm("Confirm that you want to delete this news.")) {
        await fetch('/News/Delete/' + id,
            {
                method: 'DELETE'
            }).then(res => {
                if (res.ok) {
                    const child = document.getElementById(id);

                    const parent = document.getElementById("exercises-list");

                    parent.removeChild(child);
                } else if (res.status === 404) {
                    alert('Item not found.');
                }
            });
    }
}

async function updateNews(id) {
    event.preventDefault();

    const form = document.getElementById('update-exercise');

    const formData = new FormData(form);

    let jsonData = {};

    for (let [key, value] of formData.entries()) {
        jsonData[key] = value;
    }

    await fetch('/News/Update/' + id, {
        method: "PUT",
        body: JSON.stringify(jsonData),
        headers: {
            "Content-Type": "application/json"
        }
    }).then(data => {
        window.location.href = '/News/Index';
    });
}

async function showUpdateNewsView(id) {
    await fetch('/News/Update/' + id,
        {
            method: 'GET'
        }).then(res => window.location.href = '/News/Update/' + id);
}