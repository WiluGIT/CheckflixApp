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
      '2xl': '1535px',
      '3xl': '1600px',
    },
  },
  plugins: [
    require('@tailwindcss/typography'),
    require("daisyui")
  ],
}