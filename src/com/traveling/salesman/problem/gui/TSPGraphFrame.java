package com.traveling.salesman.problem.gui;

import javax.swing.*;
import java.awt.*;

public class TSPGraphFrame extends JFrame {
    JPanel graphMainPanel;


    public TSPGraphFrame() {
        setTitle("TSP-Graph");
        createGraphMainPanel();
        setContentPane(graphMainPanel);
        setSize(1000, 800);
        setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);
        setLocationRelativeTo(null);
        setExtendedState(JFrame.MAXIMIZED_BOTH);
        test();
        graphMainPanel.repaint();
        repaint();
        setVisible(true);
    }

    public void createGraphMainPanel() {
        JPanel panel = new JPanel(){
            @Override
            public void paint(Graphics g) {
                super.paint(g);
                setBackground(Color.BLACK);

            }
        };
        this.graphMainPanel = panel;
    }

    public void test(){
        graphMainPanel.setBackground(Color.BLUE);

    }
}
