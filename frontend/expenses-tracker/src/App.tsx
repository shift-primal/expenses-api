import { Route, Routes } from "react-router";
import { HomePage } from "@pages/HomePage";
import { ImportPage } from "@pages/ImportPage";
import { NotFoundPage } from "@pages/NotFoundPage";

const App = () => {
  return (
    <Routes>
      <Route index element={<HomePage />} />
      <Route path="/import" element={<ImportPage />} />
      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  );
};

export default App;
