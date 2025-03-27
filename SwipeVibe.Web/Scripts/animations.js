document.addEventListener('DOMContentLoaded', () => {
    console.log('Animations initialized');
    initAnimations();
});


function initAnimations() {
    initGradientAnimations();
    initScrollAnimations();
    initButtonAnimations();
}


function initGradientAnimations() {
    const gradientElements = document.querySelectorAll('.gradient-icon');
    
    gradientElements.forEach(element => {
        element.classList.add('gradient-animation');
    });
}

        
function initScrollAnimations() {
    const videoCards = document.querySelectorAll('.video-card');
    
    videoCards.forEach((card, index) => {
        setTimeout(() => {
            card.style.opacity = '0';
            card.classList.add('fade-in');
        }, index * 200);
    });
}


function initButtonAnimations() {
    const buttons = document.querySelectorAll('.neomorph-btn, .neomorph-icon');
    
    buttons.forEach(button => {
        button.addEventListener('mousedown', function() {
            this.classList.add('pressed');
        });
        
        button.addEventListener('mouseup', function() {
            this.classList.remove('pressed');
        });
        
        button.addEventListener('mouseleave', function() {
            this.classList.remove('pressed');
        });
        button.addEventListener('click', function(e) {
            createRippleEffect(e, this);
        });
    });
}


function createRippleEffect(event, button) {
    const ripple = document.createElement('span');
    ripple.classList.add('ripple-effect');
    
    const buttonRect = button.getBoundingClientRect();
    
    const size = Math.max(buttonRect.width, buttonRect.height) * 2;
    
    ripple.style.width = ripple.style.height = `${size}px`;
    
    const x = event.clientX - buttonRect.left - size / 2;
    const y = event.clientY - buttonRect.top - size / 2;
    
    ripple.style.left = `${x}px`;
    ripple.style.top = `${y}px`;
    
    button.appendChild(ripple);
    
    setTimeout(() => {
        ripple.remove();
    }, 600);
}


function pulseAnimation(element, duration = 1000) {
    element.classList.add('pulse-animation');
    
    setTimeout(() => {
        element.classList.remove('pulse-animation');
    }, duration);
}


function fadeInAnimation(element, delay = 0) {
    element.style.opacity = '0';
    
    setTimeout(() => {
        element.classList.add('fade-in');
    }, delay);
}


function fadeOutAnimation(element, callback = null) {
    element.classList.add('fade-out');
    
    setTimeout(() => {
        if (callback) {
            callback();
        }
    }, 500);
} 