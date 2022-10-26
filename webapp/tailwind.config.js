/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors:{
        'primary':{
          200 : '#68B6BF',
          600 : '#2B818B',
          700 : '#0B5058'
        },
        'gray' : {
          200: '#ECF0F2',
        },
        'green':{
          600 : '#12B76A'
        },
      },
      padding : {
        22 : '84px'
      },
      fontFamily:{
        inter  : "'Inter', sans-serif"  
      },
      screens:{
        'lg': '1100px',
      }
    },
  },
  plugins: [],
}
