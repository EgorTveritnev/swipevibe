document.addEventListener('DOMContentLoaded', function() {
    const togglePasswordButtons = document.querySelectorAll('.toggle-password');
    togglePasswordButtons.forEach(button => {
        button.addEventListener('click', function() {
            const input = this.previousElementSibling;
            const type = input.getAttribute('type') === 'password' ? 'text' : 'password';
            input.setAttribute('type', type);
            this.querySelector('i').classList.toggle('fa-eye');
            this.querySelector('i').classList.toggle('fa-eye-slash');
        });
    });
    const passwordInput = document.getElementById('password');
    if (passwordInput) {
        passwordInput.addEventListener('input', function() {
            const password = this.value;
            const strength = checkPasswordStrength(password);
            updatePasswordStrength(strength);
        });
    }
    
    const registerForm = document.getElementById('registerForm');
    if (registerForm) {
        registerForm.addEventListener('submit', function(e) {
            e.preventDefault();         
            const username = document.getElementById('username').value;
            const email = document.getElementById('email').value;
            const password = document.getElementById('password').value;
            const confirmPassword = document.getElementById('confirmPassword').value;
            const terms = document.getElementById('terms').checked;            
            if (username.length < 3) {
                showError('username', 'Имя пользователя должно содержать минимум 3 символа');
                return;
            }           
            if (!isValidEmail(email)) {
                showError('email', 'Введите корректный email');
                return;
            }   
            if (password.length < 8) {
                showError('password', 'Пароль должен содержать минимум 8 символов');
                return;
            }   
            if (password !== confirmPassword) {
                showError('confirmPassword', 'Пароли не совпадают');
                return;
            }    
            if (!terms) {
                showError('terms', 'Необходимо согласиться с условиями использования');
                return;
            }      
            submitForm(this);
        });
    }
    
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', function(e) {
            e.preventDefault();        
            const email = document.getElementById('email').value;
            const password = document.getElementById('password').value;
            const remember = document.getElementById('remember').checked;         
            if (!isValidEmail(email)) {
                showError('email', 'Введите корректный email');
                return;
            }           
            if (password.length < 8) {
                showError('password', 'Пароль должен содержать минимум 8 символов');
                return;
            }           
            submitForm(this);
        });
    }
    const socialButtons = document.querySelectorAll('.social-button');
    socialButtons.forEach(button => {
        button.addEventListener('click', function() {
            const provider = this.classList[1]; 
            handleSocialAuth(provider);
        });
    });
});

function checkPasswordStrength(password) {
    let strength = 0;  
    if (password.length >= 8) strength += 1;
    if (password.length >= 12) strength += 1; 
    if (/\d/.test(password)) strength += 1;
    if (/[a-z]/.test(password)) strength += 1;   
    if (/[A-Z]/.test(password)) strength += 1;    
    if (/[^A-Za-z0-9]/.test(password)) strength += 1;   
    return strength;
}

function updatePasswordStrength(strength) {
    const strengthBar = document.querySelector('.strength-bar');
    const strengthText = document.querySelector('.strength-text');   
    const width = (strength / 6) * 100;
    strengthBar.style.setProperty('--strength-width', `${width}%`);
    let color;
    if (strength <= 2) color = '#e74c3c';
    else if (strength <= 4) color = '#f1c40f';
    else color = '#2ecc71';
    strengthBar.style.setProperty('--strength-color', color);    
    let text;
    if (strength <= 2) text = 'Слабый';
    else if (strength <= 4) text = 'Средний';
    else text = 'Сильный';
    strengthText.textContent = `Надежность пароля: ${text}`;
}

function isValidEmail(email) {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
}

function showError(fieldId, message) {
    const field = document.getElementById(fieldId);
    const errorDiv = document.createElement('div');
    errorDiv.className = 'error-message';
    errorDiv.textContent = message;   
    const existingError = field.parentElement.querySelector('.error-message');
    if (existingError) {
        existingError.remove();
    }   
    field.parentElement.appendChild(errorDiv);
    field.classList.add('error');
    
    setTimeout(() => {
        errorDiv.remove();
        field.classList.remove('error');
    }, 3000);
}

function submitForm(form) {
    console.log('Отправка формы:', new FormData(form));
}

function handleSocialAuth(provider) {
    console.log('Авторизация через:', provider);
} 