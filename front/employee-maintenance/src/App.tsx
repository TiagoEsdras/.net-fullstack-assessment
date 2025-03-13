import { BrowserRouter as Router } from "react-router-dom";
import AppRoutes from "./routes/routes";
import Header from "./components/Header";
import Footer from "./components/Footer";
import ErrorModal from './components/ErrorModal';
import { ErrorMessage } from './types/ErrorMessage';
import { useEffect, useState } from "react";
import { setupAxiosInterceptors } from "./services/axiosInterceptor";

const App = () => {
  const [modalMessage, setModalMessage] = useState<string | null>(null);
  const [modalErrors, setModalErrors] = useState<ErrorMessage[]>([]);

  useEffect(() => {
    setupAxiosInterceptors((message: string, errors: ErrorMessage[]) => {
      setModalMessage(message);
      setModalErrors(errors);
    });
  }, []);

  const closeModal = () => {
    setModalMessage(null);
    setModalErrors([]);
  };

  return (
    <div className="min-h-screen flex flex-col">
    <Router> 
      {modalMessage && (
        <ErrorModal
          message={modalMessage}
          errors={modalErrors}
          onClose={closeModal}
        />
      )}     
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