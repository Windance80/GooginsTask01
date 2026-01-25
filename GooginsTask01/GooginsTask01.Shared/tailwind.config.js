/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./**/*.razor",               // Scans all .razor files in the project
    "./**/*.cshtml",              // If you have any
    //"./wwwroot/index.html",       // Your main HTML file
    // Add any JS/TS files if you use classes there too
    // "./**/*.{js,jsx,ts,tsx}"
  ],
  // You can add theme extensions here if needed
}