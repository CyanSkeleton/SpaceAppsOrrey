const canvas = document.getElementById("starfield");
const ctx = canvas.getContext("2d");

canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

const stars = [];
const maxStars = 1000; // Number of background stars
let shootingStar = null; // Will store the shooting star

// Function to generate random background stars
function generateStars() {
	for (let i = 0; i < maxStars; i++) {
		stars.push({
			x: Math.random() * canvas.width,
			y: Math.random() * canvas.height,
			radius: Math.random() * 2 + 1, // Random size
			opacity: 0,  // Start with 0 opacity
			fadeSpeed: Math.random() * 0.02 + 0.005, // Random fade speed
			fadeDirection: 1 // 1 for fade-in, -1 for fade-out
		});
	}
}

// Function to update the background stars' fading behavior
function updateStars() {
	stars.forEach(star => {
		star.opacity += star.fadeSpeed * star.fadeDirection;

		// Reverse direction when fully visible or invisible
		if (star.opacity >= 1) {
			star.fadeDirection = -1;
		} else if (star.opacity <= 0) {
			// Once invisible, reposition the star randomly
			star.x = Math.random() * canvas.width;
			star.y = Math.random() * canvas.height;
			star.fadeDirection = 1;
		}
	});
}

// Function to draw the background stars
function drawStars() {
	stars.forEach(star => {
		ctx.beginPath();
		ctx.arc(star.x, star.y, star.radius, 0, Math.PI * 2);
		ctx.fillStyle = `rgba(255, 255, 255, ${star.opacity})`;
		ctx.fill();
	});
}

// Function to create a new shooting star
function createShootingStar() {
	const startX = Math.random() * canvas.width;
	const startY = Math.random() * canvas.height / 2; // Top half of the screen
	const endX = Math.random() * canvas.width;
	const endY = canvas.height;

	shootingStar = {
		x: startX,
		y: startY,
		dx: (endX - startX) / 50, // Velocity in X direction (50 frames)
		dy: (endY - startY) / 50, // Velocity in Y direction (50 frames)
		length: Math.random() * 80 + 50, // Shooting star length
		opacity: 1, // Shooting star starts fully visible
		trailLength: 0 // Trail will grow over time
	};
}

// Function to draw the shooting star
function drawShootingStar() {
	if (!shootingStar) return;

	ctx.beginPath();
	ctx.moveTo(shootingStar.x, shootingStar.y);
	ctx.lineTo(
		shootingStar.x - shootingStar.trailLength * shootingStar.dx, 
		shootingStar.y - shootingStar.trailLength * shootingStar.dy
	);
	ctx.strokeStyle = `rgba(255, 255, 255, ${shootingStar.opacity})`;
	ctx.lineWidth = 2;
	ctx.stroke();
}

// Function to update the shooting star's position and trail
function updateShootingStar() {
	if (!shootingStar) return;

	// Move shooting star
	shootingStar.x += shootingStar.dx;
	shootingStar.y += shootingStar.dy;

	// Grow trail length
	shootingStar.trailLength += 1;

	// Fade out the shooting star after some distance
	shootingStar.opacity -= 0.02;

	// Remove shooting star when it fades out or leaves the screen
	if (shootingStar.opacity <= 0 || shootingStar.y > canvas.height) {
		shootingStar = null;
		setTimeout(createShootingStar, Math.random() * 5000 + 5000); // Create another shooting star in 5-10 seconds
	}
}

// Main animation loop
function animate() {
	ctx.clearRect(0, 0, canvas.width, canvas.height); // Clear the canvas

	updateStars();
	drawStars();

	drawShootingStar();
	updateShootingStar();

	requestAnimationFrame(animate);
}

// Adjust canvas size when window is resized
window.addEventListener("resize", () => {
	canvas.width = window.innerWidth;
	canvas.height = window.innerHeight;
});

// Initialize stars, shooting star, and start the animation
generateStars();
setTimeout(createShootingStar, Math.random() * 5000 + 5000); // Shooting star appears within 5-10 seconds
animate();

