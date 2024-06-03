document.addEventListener("DOMContentLoaded", function() {
// Run the code immediately after the DOM content is loaded
const main_side = document.querySelector(".main-side");

const shipWarsText = document.createElement("p");
shipWarsText.textContent = "ShipWars";
shipWarsText.style.fontSize = "250px";
shipWarsText.style.position = "absolute";
shipWarsText.style.top = "20%";
shipWarsText.style.left = "50%";
shipWarsText.style.transform = "translate(-50%, -50%)";
shipWarsText.style.opacity = "0"; // Initially hide the text
shipWarsText.style.transition = "opacity 1s ease";
document.body.appendChild(shipWarsText);

// Define the time (in milliseconds) when the "ShipWars" text should appear
const textAppearTime = 10000; // Adjust this value as needed

// Start checking elapsed time immediately
const checkElapsedTime = () => {
    const startTime = performance.now();
    const check = () => {
        const elapsedTime = performance.now() - startTime;
        if (elapsedTime >= textAppearTime) {
            shipWarsText.style.opacity = "1"; // Show the "ShipWars" text
            document.body.style.overflow = "visible";
            main_side.style.visibility = "visible";
        } else {
            requestAnimationFrame(check);
        }
    };
    requestAnimationFrame(check);
};

// Run the function to check elapsed time
checkElapsedTime();

});
