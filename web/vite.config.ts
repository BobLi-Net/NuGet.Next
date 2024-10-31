import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { resolve } from 'path'

// https://vite.dev/config/
export default defineConfig({
  resolve:{
    alias: {
      '@': resolve(__dirname, './src'),
    },
    extensions:['.js','.jsx','.ts','.tsx']
  },
  plugins: [react()],
  server:{
    proxy:{
      '/api':{
        target:'http://localhost:5228/',
        changeOrigin:true,
      },
      "/v3":{
        target:'http://localhost:5228/',
        changeOrigin:true,
      }
    }
  }
})
