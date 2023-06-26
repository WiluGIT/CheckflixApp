/** @type {import('tailwindcss').Config} */
const defaultTheme = require('tailwindcss/defaultTheme')

export default {
  mode: 'jit',
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        
      }
    },
    screens: {
      'xs': '500px',
      ...defaultTheme.screens,
    },
  },
  plugins: [
    require('@tailwindcss/typography'),
    require("daisyui")
  ],
}