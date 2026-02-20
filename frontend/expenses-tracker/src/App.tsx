import { Route, Routes } from "react-router";
import { HomePage } from "./pages/HomePage";
import { ImportPage } from "./pages/ImportPage";

const App = () => {
  const isLoading: Boolean = false;

  if (isLoading) return <div>Loading...</div>;

  return (
    <Routes>
      <Route index element={<HomePage />} />
      <Route path="/import" element={<ImportPage />} />
      <Route path="*" element={<div>404</div>} />
    </Routes>
  );
};

export default App;
