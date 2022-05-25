package com.traveling.salesman.problem.main;

public class Crossover {
    public static final int CROSSOVER_VERSION_1 = 0;
    public static final int CROSSOVER_VERSION_2 = 1;
    public static final int CROSSOVER_VERSION_3 = 2;


    /**
     * This methode executes the crossover.
     * @param CROSSOVER_VERSION     Which crossover shall be executed
     * @param gene1                 The first gene for the crossover
     * @param gene2                 The second gene for the crossover
     *
     * @return Individual[]         The children of the crossover.
     */
    public static Individual[] execCrossover(int CROSSOVER_VERSION, int[] gene1, int[] gene2){
        switch (CROSSOVER_VERSION){
            case CROSSOVER_VERSION_1:
                return crossoverVer1(gene1, gene2);
            case CROSSOVER_VERSION_2:
                return crossoverVer2(gene1, gene2);
            case CROSSOVER_VERSION_3:
                return crossoverVer3(gene1, gene2);
            default:
                System.out.println("Crossover failed: Invalid CROSSOVER_VERSION: " + CROSSOVER_VERSION);
                return null;
        }
    }

    /**
     * Version 1:
     * The first parts of the parent genes are copied into the children genes.
     * The second part of the genes ot the children is filled with the missing genes of the other parent.
     *
     */
    public static Individual[] crossoverVer1(int[] gene_1, int[] gene_2){
        int length = gene_1.length;
        int[] child_1 = new int[length];
        int[] child_2 = new int[length];

        //Copying the first half of gene_1 in to child_1 and vise versa with gene_2 and child_2
        System.arraycopy(gene_1,0,child_1,0,length/2);
        System.arraycopy(gene_2,0,child_2,0,length/2);

        //Copying all missing genoms of gene_2 in child_1
        //and the same with gene_1 and child_2
        for(int i = 0,k=length/2; i<length;i++){
            int j = 0;
            for(; j<length/2;j++){
                if(child_1[j]==gene_2[i]){
                    break;
                }
            }
            if(j==length/2){
                child_1[k] = gene_2[i];
                k++;
            }
        }
        for(int i = 0,k=length/2; i<length;i++){
            int j = 0;
            for(; j<length/2;j++){
                if(child_2[j]==gene_1[i]){
                    break;
                }
            }
            if(j==length/2){
                child_2[k] = gene_1[i];
                k++;
            }
        }
        return new Individual[]{new Individual(child_1), new Individual(child_2)};
    }

    /**
     * Version 2:
     * The first part of parent_1 is copied to the first part of child_1 then the second part of parent_2 is
     * copied to the second part of child_2. Then the rest of parent_1 is filled into the space of child_2 and vise versa
     * with parent_2 and child_1
     *
     */
    public static Individual[] crossoverVer2(int[] gene_1, int[] gene_2){
        int length = gene_1.length;
        int[] child_1 = new int[length];
        int[] child_2 = new int[length];

        System.arraycopy(gene_1,0,child_1, 0,length/2 );
        System.arraycopy(gene_2,length/2, child_2,length/2, (length/2)+(length%2));

        for(int i = 0,k=length/2; i<length;i++){
            int j = 0;
            for(; j<length/2;j++){
                if(child_1[j]==gene_2[i]){
                    break;
                }
            }
            if(j==length/2){
                child_1[k] = gene_2[i];
                k++;
            }
        }
        for(int i = 0,k=0; i<length;i++){
            int j = length/2;
            for(; j<length;j++){
                if(child_2[j]==gene_1[i]){
                    break;
                }
            }
            if(j==length){
                child_2[k] = gene_1[i];
                k++;
            }
        }
        return new Individual[]{new Individual(child_1), new Individual(child_2)};
    }

    /*
     * Version 3:
     * The parent_1 copies the second half of his gene into the first half of the child_1.
     * The parent_2 copies the second half of his gene into the first half of the child_2.
     * Then child_1 gets the missing genoms from parent_2 and the child_2 from parent_1.
     *
     * Best result !
     */
    public static Individual[] crossoverVer3(int[] gene_1, int[] gene_2){
        int length = gene_1.length;
        int[] child_1 = new int[length];
        int[] child_2 = new int[length];


        System.arraycopy(gene_1,length/2,child_1,0,(length/2)+(length%2));
        System.arraycopy(gene_2,length/2,child_2,0,(length/2)+(length%2));


        for(int i = 0,k=length/2; i<length;i++){
            int j = 0;
            for(; j<length/2;j++){
                if(child_1[j]==gene_2[i]){
                    break;
                }
            }
            if(j==length/2){
                child_1[k] = gene_2[i];
                k++;
            }
        }
        for(int i = 0,k=length/2; i<length;i++){
            int j = 0;
            for(; j<length/2;j++){
                if(child_2[j]==gene_1[i]){
                    break;
                }
            }
            if(j==length/2){
                child_2[k] = gene_1[i];
                k++;
            }
        }
        return new Individual[]{new Individual(child_1), new Individual(child_2)};
    }

}
