package com.traveling.salesman.problem.main;

import java.util.Arrays;
import java.util.Objects;

/**
 *  This class is used to store the data for a route.
 *  Each {@link Individual} object represents a route which starts at one city and then passes every remaining city all
 *  the way back to the starting point. Every route has a length which indicates, the fitness of the route.
 *  The smaller the fitness is the better. The route is represented as an int array, where every entry is an ID and represents
 *  a specific city.
 */
public class Individual {
    private final int[] gene;
    private int fitness;

    /**
     * 1. Constructor:
     * Creates a new {@link Individual} based on a gene.
     * Calculates the fitness of the gene.
     * @param gene          int[] containing valid genoms
     * @param fitMatrix     int[][] containing the fitness matrix
     */
    public Individual(int[] gene, int[][] fitMatrix){
        int[] geneNeu = new int[gene.length];
        System.arraycopy(gene, 0, geneNeu, 0, gene.length);
        this.gene = geneNeu;
        this.fitness = calcFit(fitMatrix);
    }

    /**
     * 2. Constructor:
     *  Creates a new {@link Individual} which has to be filled later on.
     * @param geneLength The dimension of the gene.
     */
    public Individual(int geneLength){
        gene = new int[geneLength];
    }

    public Individual(int[] gene){
        this.gene=gene;
    }

    //Calculating the fitness of one individual
    //And updating the fitness variable.
    public int calcFit(int[][] fitMatrix){
        int fitnessNeu = 0;
        int indexX;
        int indexY;
        for(int i = 1; i< gene.length; i++){
            indexX = gene[i-1]-1;
            indexY = gene[i]-1;
            fitnessNeu += fitMatrix[indexX][indexY];
        }
        fitnessNeu += fitMatrix[gene[gene.length-1]-1][gene[0]-1];
        this.fitness= fitnessNeu;
        return fitnessNeu;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Individual that = (Individual) o;
        return fitness == that.fitness && Arrays.equals(gene, that.gene);
    }

    @Override
    public int hashCode() {
        int result = Objects.hash(fitness);
        result = 31 * result + Arrays.hashCode(gene);
        return result;
    }

    @Override
    public String toString(){
        StringBuilder retString = new StringBuilder();
        retString.append("[");
        Arrays.stream(gene).forEach(e -> retString.append(String.format("%-4d%-3s",e,"|")));
        retString.replace(retString.length()-4,retString.length(),"]");
        return retString.toString();
    }

    public int[] getGene() {
        return gene;
    }
}
