const bookApiUrl = "/api/book";

document.addEventListener("DOMContentLoaded", () => {
    loadBooks();

    document.getElementById("bookForm").addEventListener("submit", async (e) => {
        e.preventDefault();
        const title = document.getElementById("title").value;
        const author = document.getElementById("author").value;
        const price = parseFloat(document.getElementById("price").value || 0);
        const stock = parseInt(document.getElementById("stock").value || 0);

        await fetch(bookApiUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ title, author, price, stock }),
        });

        e.target.reset();
        loadBooks();
    });
});

async function loadBooks() {
    const response = await fetch(bookApiUrl);
    const books = await response.json();
    const tbody = document.querySelector("#bookTable tbody");
    tbody.innerHTML = "";

    books.forEach((book) => {
        const row = document.createElement("tr");
        row.innerHTML = `
          <td>${book.title}</td>
          <td>${book.author}</td>
          <td>$${book.price.toFixed(2)}</td>
          <td>${book.stock}</td>
          <td><button onclick="deleteBook(${book.id})">Eliminar</button></td>
        `;
        tbody.appendChild(row);
    });
}

async function deleteBook(id) {
    await fetch(`${bookApiUrl}/${id}`, { method: "DELETE" });
    loadBooks();
}
