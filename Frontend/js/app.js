// JavaScript for CRUD operations, search, and API calls

// Simulated API endpoint
const apiEndpoint = 'https://api.example.com/items';

// Function to fetch items from the API
async function fetchItems() {
    try {
        const response = await fetch(apiEndpoint);
        return await response.json();
    } catch (error) {
        console.error('Error fetching items:', error);
    }
}

// Function to add a new item
async function addItem(newItem) {
    try {
        const response = await fetch(apiEndpoint, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newItem)
        });
        return await response.json();
    } catch (error) {
        console.error('Error adding item:', error);
    }
}

// Function to update an existing item
async function updateItem(id, updatedItem) {
    try {
        const response = await fetch(`${apiEndpoint}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedItem)
        });
        return await response.json();
    } catch (error) {
        console.error('Error updating item:', error);
    }
}

// Function to delete an item
async function deleteItem(id) {
    try {
        await fetch(`${apiEndpoint}/${id}`, {
            method: 'DELETE'
        });
    } catch (error) {
        console.error('Error deleting item:', error);
    }
}

// Function to search items by name
async function searchItems(query) {
    const items = await fetchItems();
    return items.filter(item => item.name.toLowerCase().includes(query.toLowerCase()));
}

// Example usage
(async () => {
    const items = await fetchItems();
    console.log('Fetched items:', items);
    const newItem = { name: 'New Item', price: 10 };
    await addItem(newItem);
    console.log('Item added');
    const updatedItem = { name: 'Updated Item', price: 15 };
    await updateItem(1, updatedItem);
    console.log('Item updated');
    await deleteItem(1);
    console.log('Item deleted');
    const searchResults = await searchItems('item');
    console.log('Search results:', searchResults);
})();
