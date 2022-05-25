package com.traveling.salesman.problem.main;

import java.util.Random;

/**
 * This class is used to mutate  {@link Individual} objects.
 */
public class Mutation {
    public static final int MUTATION_VERSION_1 = 0;
    public static final int MUTATION_VERSION_2 = 1;
    public static int mutationProbability = 1;
    public static int mutationRange = 1;

    /**
     * This methode executes mutations.
     * @param MUTATION_VERSION  Which mutation shall be executed
     * @param individual        On which {@link Individual} the mutation shall be executed.
     */
    public static void mutate(int MUTATION_VERSION, Individual individual){
        switch (MUTATION_VERSION) {
            case MUTATION_VERSION_1 -> mutateVer1(individual);
            case MUTATION_VERSION_2 -> mutateVer2(individual);
            default -> {
            }
        }
    }

    /**
     * 1. Random Mutation :
     * For each {@link Individual} there could be a random mutation with the probability of {@value mutationProbability}.
     * If this is the case, a random genom is selected and switched with another genom at the distance of {@value mutationRange}
     * !! The fitness has to be calculated again !!
     */
    private static void mutateVer1(Individual individual){
        int[] gene = individual.getGene();
        Random rand = new Random();
        //Determine whether a mutation happens or not
        if(rand.nextInt(mutationProbability)==0){
            //Determine which genom is switched with another genome
            int index = rand.nextInt(gene.length);
            int tempGenom = gene[index];
            //Swapping the genoms, if the index + mutationSize ist greater than the length of the gene array,
            //the index starts at zero and the rest is added as the new index.
            if(index+mutationRange < gene.length) {
                gene[index] = gene[index + mutationRange];
                gene[index + mutationRange] = tempGenom;
            } else{
                gene[index] = gene[(index+mutationRange)%gene.length];
                gene[(index+mutationRange)%gene.length] = tempGenom;
            }
        }
    }

    /**
     * 2. Random Mutation :
     * Iterating through the gene of each {@link Individual} and with a probability of {@value mutationProbability} the current genom
     * is switched with another genom at the distance of {@value mutationRange}.
     * !! The fitness has to be calculated again !!
     */
    public static void mutateVer2(Individual individual){
        int[] gene = individual.getGene();
        Random rand = new Random();
        int tmpGenom;
        for(int i=0;i<individual.getGene().length;i++){
            if(rand.nextInt(0,100)<mutationProbability){
                tmpGenom=gene[i];
                if(i+mutationRange < gene.length) {
                    gene[i] = gene[i + mutationRange];
                    gene[i + mutationRange] = tmpGenom;
                } else{
                    gene[i] = gene[(i+mutationRange)%gene.length];
                    gene[(i+mutationRange)%gene.length] = tmpGenom;
                }
            }
        }

    }
}
