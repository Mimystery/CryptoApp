import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
   allowedHosts: [
      'crypto-app-ukgs.vercel.app',
      '2854-31-182-61-2.ngrok-free.app'
    ],
  },
})
