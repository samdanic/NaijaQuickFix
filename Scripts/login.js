
const username = document.getElementById('username');
const password = document.getElementById('password');
$(document).ready(async () => {
    $('.login-btn').on('click', async (event) => loginUser(event) )
})

async function validateInput() {
    let valid = true
    const usernameError = {};
    const passwordError = {};
    if (username.value === '') {
        usernameError.empty = 'Username / Email is empty'
        valid = false
    }
    if (password.value === '') {
        passwordError.empty = 'Password is empty';
        valid = false
    }
    return {
        valid,
        usernameError,
        passwordError
    }
}

async function loginUser(event) {
    event.preventDefault();
    const { valid, passwordError, usernameError } = await validateInput();
    const  value = await validateInput();
    console.log('Validating', valid, value);
    if (!valid) {
        alert('Fields are empty');
    }
    else {
        alert('Login Successful')
    }
}