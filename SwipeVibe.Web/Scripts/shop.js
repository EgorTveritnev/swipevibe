document.addEventListener('DOMContentLoaded', function() {
    const tabButtons = document.querySelectorAll('.tab-btn');
    const tabContents = document.querySelectorAll('.tab-content');
    
    tabButtons.forEach(button => {
        button.addEventListener('click', () => {
            tabButtons.forEach(btn => btn.classList.remove('active'));
            tabContents.forEach(content => content.classList.remove('active'));
            
            button.classList.add('active');
            
            const tabName = button.getAttribute('data-tab');
            document.getElementById(`${tabName}-content`).classList.add('active');
        });
    });
    
    function animatePricingCards() {
        const cards = document.querySelectorAll('.pricing-card');
        cards.forEach((card, index) => {
            card.style.opacity = '0';
            card.style.transform = 'translateY(20px)';
            card.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
            
            setTimeout(() => {
                card.style.opacity = '1';
                card.style.transform = 'translateY(0)';
            }, 100 * index);
        });
    }
    
    function animateCoinPackages() {
        const packages = document.querySelectorAll('.coin-package');
        packages.forEach((pkg, index) => {
            pkg.style.opacity = '0';
            pkg.style.transform = 'translateY(20px)';
            pkg.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
            
            setTimeout(() => {
                pkg.style.opacity = '1';
                pkg.style.transform = 'translateY(0)';
            }, 100 * index);
        });
    }
    
    animatePricingCards();
    animateCoinPackages();
    
    const pricingButtons = document.querySelectorAll('.pricing-btn');
    pricingButtons.forEach(button => {
        button.addEventListener('click', handlePurchaseClick);
    });
    
    const packageButtons = document.querySelectorAll('.package-btn');
    packageButtons.forEach(button => {
        button.addEventListener('click', handlePurchaseClick);
    });
    
    const premiumBtn = document.querySelector('.premium-btn');
    if (premiumBtn) {
        premiumBtn.addEventListener('click', handlePurchaseClick);
    }
    
    const promotionButtons = document.querySelectorAll('.promotion-btn');
    promotionButtons.forEach(button => {
        button.addEventListener('click', handlePromoClick);
    });
    
    function handlePurchaseClick(e) {
        e.preventDefault();
        
        const buttonText = this.textContent.trim();
        let productName = "";
        
        if (this.classList.contains('pricing-btn')) {
            const card = this.closest('.pricing-card');
            productName = card.querySelector('.pricing-title').textContent;
        } else if (this.classList.contains('package-btn')) {
            const pkg = this.closest('.coin-package');
            const amount = pkg.querySelector('.package-amount span').textContent;
            productName = `${amount} монет`;
        } else if (this.classList.contains('premium-btn') && !this.classList.contains('pricing-btn') && !this.classList.contains('package-btn')) {
            productName = "SwipeVibe Premium";
        }
        
        showToast(`Вы выбрали: ${productName}. Переход к оплате...`);
        
        this.style.transform = 'scale(0.95)';
        setTimeout(() => {
            this.style.transform = 'scale(1)';
        }, 200);
    }
    
    function handlePromoClick(e) {
        e.preventDefault();
        
        const promo = this.closest('.promotion-card');
        const promoTitle = promo.querySelector('h4').textContent;
        
        showToast(`Выбрана акция: ${promoTitle}`);
        
        this.style.transform = 'scale(0.95)';
        setTimeout(() => {
            this.style.transform = 'scale(1)';
        }, 200);
    }
    
    function showToast(message) {
        const toastContainer = document.querySelector('.toast-container');
        if (!toastContainer) return;
        
        const toast = document.createElement('div');
        toast.className = 'toast-notification';
        toast.innerHTML = `
            <div class="toast-icon">
                <i class="fas fa-shopping-cart"></i>
            </div>
            <div class="toast-content">
                <div class="toast-message">${message}</div>
            </div>
            <div class="toast-close">
                <i class="fas fa-times"></i>
            </div>
        `;
        
        toastContainer.appendChild(toast);
        
        setTimeout(() => {
            toast.classList.add('show');
        }, 10);
        
        setTimeout(() => {
            toast.classList.remove('show');
            setTimeout(() => {
                toast.remove();
            }, 300);
        }, 5000);
        
        const closeButton = toast.querySelector('.toast-close');
        closeButton.addEventListener('click', () => {
            toast.classList.remove('show');
            setTimeout(() => {
                toast.remove();
            }, 300);
        });
    }
    
}); 