import type { Metadata } from "next";
import { DM_Sans,Space_Grotesk } from "next/font/google";
import "./globals.css";
import Provider from "@/components/provider";

const dmSans = DM_Sans({ variable: "--font-dm-sans", subsets: ["latin"] });
const spaceGrotesk = Space_Grotesk({ variable: "--font-space-grotesk", subsets: ["latin"] });

export const metadata: Metadata = {
  title: "TaskPRO",
  description: "TaskPRO is a task management application built with Next.js, React, and TypeScript.",
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en" className={`${dmSans.variable} ${spaceGrotesk.variable} antialized`}>
      <body>
        <Provider>{children}</Provider>
      </body>
    </html>
  );
}