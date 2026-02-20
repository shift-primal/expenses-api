import { PageLayout } from "@/components/custom/PageLayout";
import { Button } from "@/components/ui/button";
import { Field, FieldDescription, FieldLabel } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { useImport } from "@/hooks/useImport";
import { useState } from "react";

export const ImportPage = () => {
  const { mutate, isPending } = useImport();
  const [file, setFile] = useState<File | null>(null);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFile(e.target.files?.[0] ?? null);
  };

  const handleFileUpload = () => {
    if (file) mutate(file);
  };

  return (
    <PageLayout>
      <div
        id="upload-section"
        className="border-2 border-black flex w-max h-max p-12 self-center mx-auto flex-col gap-y-8"
      >
        <Field>
          <FieldLabel htmlFor="file-upload">Last opp fil...</FieldLabel>
          <Input
            id="file-upload"
            type="file"
            className="cursor-pointer"
            onChange={handleFileChange}
            accept=".csv, .txt"
          />
          <FieldDescription>.csv, .txt</FieldDescription>
        </Field>
        <Button
          disabled={!file || isPending}
          className="cursor-pointer"
          onClick={handleFileUpload}
        >
          {isPending ? "Laster opp..." : "Last opp"}
        </Button>
      </div>
    </PageLayout>
  );
};
