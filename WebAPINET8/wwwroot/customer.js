const customerApiUrl = "/api/customer";

document.addEventListener("DOMContentLoaded", () => {
    loadCustomers();

    document.getElementById("customerForm").addEventListener("submit", async (e) => {
        e.preventDefault();
        const name = document.getElementById("name").value;
        const email = document.getElementById("email").value;

        await fetch(customerApiUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ name, email }),
        });

        e.target.reset();
        loadCustomers();
    });
});


async function loadCustomers() {
    const response = await fetch(customerApiUrl);
    const customers = await response.json();
    const tbody = document.querySelector("#customerTable tbody");
    tbody.innerHTML = "";

    customers.forEach((c) => {
        const row = document.createElement("tr");
        row.innerHTML = `
          <td>${c.name}</td>
          <td>${c.email}</td>
          <td><button onclick="deleteCustomer(${c.id})">Eliminar</button></td>
        `;
        tbody.appendChild(row);
    });
}

async function deleteCustomer(id) {
    await fetch(`${customerApiUrl}/${id}`, { method: "DELETE" });
    loadCustomers();
}