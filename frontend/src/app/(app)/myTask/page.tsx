"use client";

import ProgressCards from "@/components/myTask/StatCards";
import StatCards from "@/components/myTask/StatCards";
import { mockProgress } from "@/lib/mock";

export default function myTaskPage (){
    return(
        <div className="space-y-6">
              <section className="grid grid-cols-[repeat(auto-fit,minmax(220px,1fr))] gap-4">
                <ProgressCards {...mockProgress}/>
              </section>
        </div>
      
    );
}