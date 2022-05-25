package com.traveling.salesman.problem.main;

import java.util.Random;

public class Shuffle {
    public static void shuffle(int[] array){
        Random rand = new Random();
        int count = array.length;
        for(int i = count; i>1; i--){
            swap(array, i-1,rand.nextInt(i));
        }
    }

    private static void swap(int[] array, int i, int j){
        int tmp = array[i];
        array[i] =array[j];
        array[j] = tmp;
    }
}