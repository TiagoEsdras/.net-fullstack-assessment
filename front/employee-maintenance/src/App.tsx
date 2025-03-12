import { BrowserRouter as Router } from "react-router-dom"; // Corrigido para BrowserRouter
import AppRoutes from "./routes/routes";
import Header from "./components/Header";
import Footer from "./components/Footer";

const App = () => {
  return (
    <div className="min-h-screen flex flex-col">
    <Router>      
      <Header />
      <div className="flex-grow">
        <AppRoutes />
      </div>
      <Footer />
    </Router>
    </div>    
  );
};

export default App;