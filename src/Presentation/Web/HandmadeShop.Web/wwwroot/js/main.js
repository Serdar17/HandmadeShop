document.querySelectorAll('span[id^="reviews"]').forEach(link =>{
    
    link.addEventListener('click', function (e){
        e.preventDefault();
        console.log('click')
        let href = this.getAttribute('id').substring(1);
        const scrollTarget = document.getElementById(href);
        
        const topOffset = document.querySelector('.scrollto').offsetHeight;
        const elementPosition = scrollTarget.getBoundingClientRect().top;
        const offsetPosition = elementPosition - topOffset;
        
        window.scrollBy({
            top: offsetPosition,
            behavior: 'smooth'
        });
    });
});

const a = document.getElementById('reviews');
console.log(a);
