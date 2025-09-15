
        // إضافة الأنماط ديناميكياً لتجنب مشكلة قسم الـ Styles
    document.addEventListener('DOMContentLoaded', function() {
            // إنشاء عنصر style وإضافة التنسيقات إليه
            const styleElement = document.createElement('style');
    styleElement.textContent = `
    /* أنيميشن لظهور الأقسام */
    .hidden-section {
        opacity: 0;
    transform: translateY(50px);
    transition: opacity 0.8s ease, transform 0.8s ease;
                }

    .show-section {
        opacity: 1;
    transform: translateY(0);
                }

    /* تنسيقات إضافية للقسم الجديد */
    .our-products {
        background - color: #f8f9fa;
    border-radius: 15px;
                }

    /* تنسيقات البانر */
    .banner-section {
        margin: 4rem 0;
                }

    .banner-image {
        height: 400px;
    object-fit: cover;
                }

    .banner-overlay {
        position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(rgba(0,0,0,0.3), rgba(0,0,0,0.5));
                }

    .banner-content {
        z - index: 10;
    width: 80%;
                }

    .banner-content h2 {
        text - shadow: 2px 2px 4px rgba(0,0,0,0.7);
                }

    .banner-content p {
        text - shadow: 1px 1px 3px rgba(0,0,0,0.7);
                }

    media (max-width: 768px) {
                    .banner - image {
        height: 300px;
                    }

    .banner-content h2 {
        font - size: 2rem;
                    }

    .banner-content p {
        font - size: 1rem;
                    }
                }
    `;

    // إضافة الأنماط إلى head المستند
    document.head.appendChild(styleElement);
        });

    // basic slider init (if not already in fitness.js)
    (function () {
            const slides = document.querySelectorAll('.slide');
    const dots = document.querySelectorAll('.slider-dot');
    const arrows = document.querySelectorAll('.slider-arrow');
    let current = 0;
    let interval = setInterval(nextSlide, 5000);

    function show(i) {
        slides.forEach(s => s.classList.remove('active'));
                dots.forEach(d => d.classList.remove('active'));
    slides[i].classList.add('active');
    dots[i].classList.add('active');
    current = i;
            }
    function nextSlide() {show((current + 1) % slides.length); }
    function prevSlide() {show((current - 1 + slides.length) % slides.length); }

            dots.forEach((d, idx) => d.addEventListener('click', () => {show(idx); reset(); }));
            arrows[0].addEventListener('click', () => {prevSlide(); reset(); });
            arrows[1].addEventListener('click', () => {nextSlide(); reset(); });

    function reset() {clearInterval(interval); interval = setInterval(nextSlide, 5000); }
        })();

        // كود الظهور التدريجي للأقسام
        document.addEventListener("DOMContentLoaded", () => {
            const hiddenSections = document.querySelectorAll(".hidden-section");

            const observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add("show-section");
                observer.unobserve(entry.target); // يظهر مرة واحدة بس
            }
        });
            }, {threshold: 0.2 }); // 20% من العنصر يكفي عشان يتفعل

            hiddenSections.forEach(section => {
        observer.observe(section);
            });
        });
