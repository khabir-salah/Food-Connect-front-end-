function enableEditing() {
    document.querySelectorAll('#profileForm input, #profileForm textarea').forEach(function (element) {
        element.removeAttribute('readonly');
        element.removeAttribute('disabled');
        element.style.backgroundColor = '#fff';
    });

    // Hide the Edit button and show the Save button
    document.querySelector(".edit-btn").style.display = "none";
    document.querySelector(".save-btn").style.display = "inline-block";
}

function saveChanges() {
    const formElement = document.getElementById('profileForm');
    const formData = new FormData(formElement); // Use FormData to handle file uploads

    fetch('/IndividualProfile', { 
        method: 'POST',
        body: formData 
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            throw new Error('Network response was not ok.');
        })
        .then(data => {
            console.log('Success:', data);
            // Optionally, redirect or update the UI
            window.location.href = '/IndividualProfile';
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}


function saveChanges() {
    // Create the model to send to the backend
    const backendModel = {
        City: document.getElementById('city').value,
        LocalGovernment: document.getElementById('localGovernment').value,
        PostalCode: document.getElementById('postalCode').value,
        ProfileImage: document.getElementById('profileImage').files[0], // For file input
        PhoneNumber: document.getElementById('phoneNumber').value,
        Address: document.getElementById('address').value
    };

    // Send the data to the backend via AJAX or form submission
    fetch('/YourPostActionUrl', {
        method: 'POST',
        body: JSON.stringify(backendModel),
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            // Optionally, redirect or update the UI
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

