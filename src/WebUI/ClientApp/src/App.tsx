import './App.css'
import Router from './routes';
import { BrowserRouter } from 'react-router-dom'
import Layout from './pages/Layout/Layout';
import { Suspense } from 'react';


function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Suspense fallback={<h2>Loading...</h2>}>
          <Router />
        </Suspense>
      </Layout>
    </BrowserRouter>
  )
}

export default App
