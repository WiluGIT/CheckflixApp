import './App.css'
import Router from './routes';
import { BrowserRouter } from 'react-router-dom'
import Layout from './pages/Layout/Layout';
import { Suspense } from 'react';
import { AuthContextProvider } from './context/AuthContextProvider';

function App() {
  return (
    <BrowserRouter>
      <AuthContextProvider>
        <Layout>
          <Suspense fallback={<h2>Loading...</h2>}>
            <Router />
          </Suspense>
        </Layout>
      </AuthContextProvider>
    </BrowserRouter>
  )
}

export default App
