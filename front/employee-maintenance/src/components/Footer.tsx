const Footer = () => {
  return (
    <footer className="bg-gray-100 py-4 mt-10">
      <div className="container mx-auto text-center text-gray-500">
        <p>&copy; {new Date().getFullYear()} Employee Maintenance. All rights reserved.</p>
        <p>
          Made with ❤️ by Tiago Esdras.
        </p>
      </div>
    </footer>
  );
};

export default Footer;
