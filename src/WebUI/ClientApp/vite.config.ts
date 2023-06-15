import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import { resolve } from 'path';

function pathResolve(dir: string) {
  return resolve(__dirname, '.', dir);
}

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: [
      {
        // /@/xxxx  =>  src/xxx
        find: /^~/,
        replacement: pathResolve('node_modules') + '/',
      },
      {
        // /@/xxxx  =>  src/xxx
        find: /@\//,
        replacement: pathResolve('src') + '/',
      },
    ],
  }
})
