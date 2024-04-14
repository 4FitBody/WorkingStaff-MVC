function addMuscle() {
    const newMuscle = document.getElementById("muscle-array-input");

    const list = document.getElementById("secondary-muscles-display");

    if (newMuscle.value == "") {
        alert("Enter a muscle name!");

        return;
    }

    list.value += newMuscle.value + "; ";

    newMuscle.value = "";
}

function addInstruction() {
    const newInstruction = document.getElementById("instruction-array-input");

    const list = document.getElementById("instruction-display");

    if (newInstruction.value == "") {
        alert("Enter an instruction step!");

        return;
    }

    list.value += newInstruction.value + "; ";

    newInstruction.value = "";
}

async function deleteExercise(id) {
    if (confirm("Confirm that you want to delete this exercise.")) {
        await fetch('/Exercise/Delete/' + id,
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

async function updateExercise(id) {
    event.preventDefault();

    const form = document.getElementById('update-exercise');

    const formData = new FormData(form);

    let jsonData = {};

    for (let [key, value] of formData.entries()) {
        jsonData[key] = value;
    }

    await fetch('/Exercise/Update/' + id, {
        method: "PUT",
        body: JSON.stringify(jsonData),
        headers: {
            "Content-Type": "application/json"
        }
    }).then(data => {
        window.location.href = '/Exercise/Index';
    });
}

async function showUpdateExerciseView(id) {
    await fetch('/Exercise/Update/' + id,
        {
            method: 'GET'
        }).then(res => window.location.href = '/Exercise/Update/' + id);
}