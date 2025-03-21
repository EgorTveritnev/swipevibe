document.addEventListener('DOMContentLoaded', function() {
    const errorCode = document.querySelector('.error-code');
    errorCode.classList.add('animate-entrance');
    
    const pageCards = document.querySelectorAll('.page-card');
    pageCards.forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-5px)';
        });      
        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0)';
        });
    });
    
    const searchInput = document.querySelector('.search-input input');
    searchInput.addEventListener('keypress', function(e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            const query = this.value.trim();
            if (query) {
                performSearch(query);
            }
        }
    });   
    const backButton = document.querySelector('.back-button');
    backButton.addEventListener('mouseenter', function() {
        this.style.transform = 'translateY(-3px)';
    });    
    backButton.addEventListener('mouseleave', function() {
        this.style.transform = 'translateY(0)';
    });
});

function performSearch(query) {
    console.log('Поиск по запросу:', query);    
    setTimeout(() => {
        window.location.href = 'index.html';
    }, 500);
} 