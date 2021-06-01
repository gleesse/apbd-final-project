module.exports = {
  purge: {
    enabled: true,
    content: [
        './**/*.html',
        './**/*.razor'
    ],
},
  darkMode: 'class',
  theme: {
    extend: {
      backgroundImage: theme => ({
        'waves-pattern': "url('./images/header-bg.svg')",
       }),
       boxShadow: {
         'hover': '0px 10px 35px 2px rgba(0, 0, 0, 0.2);'
       },
       colors: {
        light: {
          background: '#eaeaea',
          highlight: '#ff2e63',
          tint: '#E1BEE7',
          primary: {
            300:'#393e46',
            400:'#222831',
          },
          secondary:{
            100:'#09DBDA',
            200:'#00C4CF',
            300:'#00adb5',
            400:'#007C82',
            500:'#006F75',
            600:'#00575C'
          }
        },
      },
    },
    fontFamily: {
      ubuntu: ['Ubuntu'],
      roboto: ['Roboto']
    }
  },
  variants: {
    extend: {
      textColor: ['active'],
    },
  },
  plugins: [],
}
