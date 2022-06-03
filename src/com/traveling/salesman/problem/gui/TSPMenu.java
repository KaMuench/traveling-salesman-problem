package com.traveling.salesman.problem.gui;

import com.intellij.uiDesigner.core.GridConstraints;
import com.intellij.uiDesigner.core.GridLayoutManager;

import javax.swing.*;
import javax.swing.border.TitledBorder;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;
import java.awt.*;
import java.lang.reflect.Method;
import java.util.ResourceBundle;
import java.util.concurrent.atomic.AtomicBoolean;

public class TSPMenu extends JFrame {

    private JTextField thisIsATextTextField;
    private JPanel mainPanel;
    private JTextPane txtDescription;
    private JLabel labelMutate;
    private JLabel labelCrossover;
    private JLabel labelPath;
    private JTextField txtPath;
    private JSlider sliderMutProb;
    private JTextPane txtMutVer1;
    private JTextPane txtCrossVer1;
    private JRadioButton radBtnMutVer1;
    private JRadioButton radBtnCrossVer1;
    private JRadioButton radBtnCrossVer2;
    private JRadioButton radBtnMutVer2;
    private JTextPane txtCrossVer2;
    private JTextPane txtMutVer2;
    private JLabel labelMutVer;
    private JLabel labelCrossVer;
    private JRadioButton radBtnCrossVer3;
    private JTextPane txtCrossVer3;
    private JPanel settingsPanel;
    private JTextPane txtConsolOutput;
    private JScrollPane consolScrollPane;
    private JPanel panelMutProb;
    private JTextField txtMutProbValue;
    private JSlider sliderMutRange;
    private JLabel labelMutRange;
    private JTextField txtMutRangValue;
    private JPanel panelMutRange;
    private JButton btnStart;
    private JLabel labelPop;
    private JTextPane txtPopPar;
    private JSlider sliderPop;
    private JPanel panelPop;
    private JLabel labelPopPanel;
    private JTextField txtPopSize;
    private JSlider sliderParents;
    private JPanel panelParents;
    private JLabel labelParents;
    private JTextField txtParentsAmount;
    private ButtonGroup btnGroupMut;
    private ButtonGroup btnGroupCross;
    private AtomicBoolean atBoolean;

    public static void main(String[] args) {
        EventQueue.invokeLater(new Runnable() {
            @Override
            public void run() {
                TSPMenu menu = new TSPMenu();
            }
        });
    }

    public TSPMenu() {
        setContentPane(mainPanel);
        setTitle("TSP MENU");
        setSize(1000, 800);
        setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);
        setVisible(true);


        //Setting up txtMutProb and txtMutRange
        txtMutProbValue.setText(String.valueOf(sliderMutProb.getValue()));
        txtMutRangValue.setText(String.valueOf(sliderMutRange.getValue()));
        txtPopSize.setText(String.valueOf(sliderPop.getValue()));


        //Setting up action listener
        atBoolean = new AtomicBoolean(true);
        btnStart.addActionListener(action -> {

        });
        sliderMutProb.addChangeListener(action -> {
            sliderAction(sliderMutProb, txtMutProbValue);
        });
        txtMutProbValue.getDocument().addDocumentListener(txtAction(sliderMutProb, txtMutProbValue));
        sliderMutRange.addChangeListener(action -> {
            txtMutRangValue.setText(String.valueOf(sliderMutRange.getValue()));
        });
        sliderPop.addChangeListener(action -> {
            txtPopSize.setText(String.valueOf(sliderPop.getValue()));
        });
        sliderParents.addChangeListener(action -> {
            txtParentsAmount.setText(String.valueOf(sliderParents.getValue()));
        });

        //Setting up ButtonGroup for Mutation and Crossover
        btnGroupCross = new ButtonGroup();
        btnGroupMut = new ButtonGroup();
        btnGroupMut.add(radBtnMutVer1);
        btnGroupMut.add(radBtnMutVer2);
        btnGroupCross.add(radBtnCrossVer1);
        btnGroupCross.add(radBtnCrossVer2);
        btnGroupCross.add(radBtnCrossVer3);
    }

    private void sliderAction(JSlider slider, JTextField txtField) {
        if (atBoolean.compareAndExchange(true, false)) {
            txtField.setText(String.valueOf(slider.getValue()));
            atBoolean.set(true);
        }
    }

    private DocumentListener txtAction(JSlider slider, JTextField txtField) {


        DocumentListener retDoc = new DocumentListener() {
            @Override
            public void insertUpdate(DocumentEvent e) {
                common(e);
            }

            @Override
            public void removeUpdate(DocumentEvent e) {
                common(e);
            }

            @Override
            public void changedUpdate(DocumentEvent e) {

            }

            private void common(DocumentEvent e) {
                if (atBoolean.compareAndExchange(true, false)) {
                    try {
                        int num = Integer.parseInt(txtField.getText());
                        slider.setValue(num);
                        atBoolean.set(true);
                    } catch (NumberFormatException nfex) {
                        atBoolean.set(true);
                    }
                }
            }
        };
        return retDoc;
    }


    {
// GUI initializer generated by IntelliJ IDEA GUI Designer
// >>> IMPORTANT!! <<<
// DO NOT EDIT OR ADD ANY CODE HERE!
        $$$setupUI$$$();
    }

    /**
     * Method generated by IntelliJ IDEA GUI Designer
     * >>> IMPORTANT!! <<<
     * DO NOT edit this method OR call it in your code!
     *
     * @noinspection ALL
     */
    private void $$$setupUI$$$() {
        mainPanel = new JPanel();
        mainPanel.setLayout(new GridLayoutManager(3, 1, new Insets(3, 3, 3, 3), -1, -1));
        mainPanel.setAlignmentX(0.0f);
        mainPanel.setAlignmentY(1.0f);
        mainPanel.setAutoscrolls(false);
        mainPanel.setBackground(new Color(-2565928));
        mainPanel.setMaximumSize(new Dimension(-1, -1));
        mainPanel.setMinimumSize(new Dimension(700, 700));
        mainPanel.setPreferredSize(new Dimension(1000, 800));
        consolScrollPane = new JScrollPane();
        consolScrollPane.setEnabled(true);
        consolScrollPane.setMinimumSize(new Dimension(-1, 100));
        consolScrollPane.setName("");
        consolScrollPane.setPreferredSize(new Dimension(-1, 100));
        consolScrollPane.setToolTipText("");
        mainPanel.add(consolScrollPane, new GridConstraints(2, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, false));
        txtConsolOutput = new JTextPane();
        txtConsolOutput.setEditable(false);
        txtConsolOutput.setMargin(new Insets(5, 5, 5, 5));
        txtConsolOutput.setMinimumSize(new Dimension(-1, 100));
        txtConsolOutput.setPreferredSize(new Dimension(-1, 100));
        txtConsolOutput.setText("Dies ist ein langer Text\n- mit vielen Absätzen\n- mit vielen Absätzen\n- mit vielen Absätzen\n- mit vielen Absätzen");
        consolScrollPane.setViewportView(txtConsolOutput);
        txtDescription = new JTextPane();
        txtDescription.setBackground(new Color(-2565928));
        txtDescription.setMaximumSize(new Dimension(-1, -1));
        txtDescription.setPreferredSize(new Dimension(-1, 22));
        txtDescription.setText(this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.txtDescription"));
        mainPanel.add(txtDescription, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_NORTH, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_CAN_GROW, new Dimension(100, 100), new Dimension(100, 50), null, 0, false));
        settingsPanel = new JPanel();
        settingsPanel.setLayout(new GridLayoutManager(11, 3, new Insets(0, 5, 5, 5), -1, -1));
        mainPanel.add(settingsPanel, new GridConstraints(1, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_GROW, new Dimension(-1, 400), new Dimension(-1, 500), null, 0, false));
        settingsPanel.setBorder(BorderFactory.createTitledBorder(null, "Settings", TitledBorder.DEFAULT_JUSTIFICATION, TitledBorder.DEFAULT_POSITION, null, null));
        labelCrossover = new JLabel();
        this.$$$loadLabelText$$$(labelCrossover, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.label.crossover"));
        settingsPanel.add(labelCrossover, new GridConstraints(0, 2, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(220, 16), null, 0, false));
        labelMutate = new JLabel();
        this.$$$loadLabelText$$$(labelMutate, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.label.mutate"));
        settingsPanel.add(labelMutate, new GridConstraints(0, 1, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(220, 16), null, 0, false));
        labelPath = new JLabel();
        labelPath.setAlignmentX(0.5f);
        this.$$$loadLabelText$$$(labelPath, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.label.path"));
        settingsPanel.add(labelPath, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(220, 16), null, 0, false));
        txtPath = new JTextField();
        settingsPanel.add(txtPath, new GridConstraints(2, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(220, 34), null, 0, false));
        labelMutVer = new JLabel();
        this.$$$loadLabelText$$$(labelMutVer, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.label.mutateVer"));
        settingsPanel.add(labelMutVer, new GridConstraints(1, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(219, 16), null, 0, false));
        labelCrossVer = new JLabel();
        this.$$$loadLabelText$$$(labelCrossVer, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.label.crossoverVer"));
        settingsPanel.add(labelCrossVer, new GridConstraints(1, 2, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtMutVer1 = new JTextPane();
        txtMutVer1.setEditable(false);
        txtMutVer1.setText("");
        settingsPanel.add(txtMutVer1, new GridConstraints(2, 1, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_WANT_GROW, null, new Dimension(150, 50), null, 0, false));
        txtCrossVer1 = new JTextPane();
        txtCrossVer1.setEditable(false);
        txtCrossVer1.setText("");
        settingsPanel.add(txtCrossVer1, new GridConstraints(2, 2, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_WANT_GROW, null, new Dimension(150, 50), null, 0, false));
        radBtnMutVer1 = new JRadioButton();
        this.$$$loadButtonText$$$(radBtnMutVer1, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.radBox.mutateVer.first"));
        settingsPanel.add(radBtnMutVer1, new GridConstraints(3, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        radBtnCrossVer1 = new JRadioButton();
        this.$$$loadButtonText$$$(radBtnCrossVer1, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.radBox.crossoverVer.first"));
        settingsPanel.add(radBtnCrossVer1, new GridConstraints(3, 2, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        radBtnCrossVer2 = new JRadioButton();
        this.$$$loadButtonText$$$(radBtnCrossVer2, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.radBox.crossoverVer.second"));
        settingsPanel.add(radBtnCrossVer2, new GridConstraints(5, 2, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        radBtnMutVer2 = new JRadioButton();
        this.$$$loadButtonText$$$(radBtnMutVer2, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.radBox.mutateVer.second"));
        settingsPanel.add(radBtnMutVer2, new GridConstraints(5, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtCrossVer2 = new JTextPane();
        txtCrossVer2.setEditable(false);
        settingsPanel.add(txtCrossVer2, new GridConstraints(4, 2, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_WANT_GROW, null, new Dimension(150, 50), null, 0, false));
        txtMutVer2 = new JTextPane();
        txtMutVer2.setEditable(false);
        txtMutVer2.setText("");
        settingsPanel.add(txtMutVer2, new GridConstraints(4, 1, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_WANT_GROW, null, new Dimension(150, 50), null, 0, false));
        radBtnCrossVer3 = new JRadioButton();
        this.$$$loadButtonText$$$(radBtnCrossVer3, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.radBox.crossoverVer.third"));
        settingsPanel.add(radBtnCrossVer3, new GridConstraints(7, 2, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtCrossVer3 = new JTextPane();
        txtCrossVer3.setEditable(false);
        settingsPanel.add(txtCrossVer3, new GridConstraints(6, 2, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_WANT_GROW, null, new Dimension(150, 50), null, 0, false));
        panelMutProb = new JPanel();
        panelMutProb.setLayout(new GridLayoutManager(1, 2, new Insets(0, 0, 0, 0), -1, -1));
        settingsPanel.add(panelMutProb, new GridConstraints(8, 1, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, false));
        final JLabel label1 = new JLabel();
        this.$$$loadLabelText$$$(label1, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.label.mutateProb"));
        panelMutProb.add(label1, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtMutProbValue = new JTextField();
        txtMutProbValue.setEditable(true);
        panelMutProb.add(txtMutProbValue, new GridConstraints(0, 1, 1, 1, GridConstraints.ANCHOR_EAST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, new Dimension(50, 34), new Dimension(50, 34), null, 0, false));
        sliderMutRange = new JSlider();
        sliderMutRange.setMaximum(10);
        sliderMutRange.setValue(5);
        settingsPanel.add(sliderMutRange, new GridConstraints(7, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        panelMutRange = new JPanel();
        panelMutRange.setLayout(new GridLayoutManager(1, 2, new Insets(0, 0, 0, 0), -1, -1));
        settingsPanel.add(panelMutRange, new GridConstraints(6, 1, 1, 1, GridConstraints.ANCHOR_SOUTH, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, false));
        labelMutRange = new JLabel();
        this.$$$loadLabelText$$$(labelMutRange, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.label.mutateRange"));
        panelMutRange.add(labelMutRange, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtMutRangValue = new JTextField();
        txtMutRangValue.setEditable(false);
        txtMutRangValue.setText("");
        panelMutRange.add(txtMutRangValue, new GridConstraints(0, 1, 1, 1, GridConstraints.ANCHOR_EAST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, new Dimension(50, 34), new Dimension(50, 34), null, 0, false));
        sliderMutProb = new JSlider();
        sliderMutProb.setSnapToTicks(false);
        sliderMutProb.setValue(50);
        sliderMutProb.setValueIsAdjusting(false);
        settingsPanel.add(sliderMutProb, new GridConstraints(9, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(219, 24), null, 0, false));
        btnStart = new JButton();
        this.$$$loadButtonText$$$(btnStart, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.btn.start"));
        settingsPanel.add(btnStart, new GridConstraints(10, 2, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        labelPop = new JLabel();
        this.$$$loadLabelText$$$(labelPop, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.label.pop"));
        settingsPanel.add(labelPop, new GridConstraints(3, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_FIXED, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtPopPar = new JTextPane();
        txtPopPar.setText("");
        settingsPanel.add(txtPopPar, new GridConstraints(4, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_WANT_GROW, null, new Dimension(150, 50), null, 0, false));
        sliderPop = new JSlider();
        sliderPop.setMaximum(1000);
        sliderPop.setMinimum(4);
        sliderPop.setMinorTickSpacing(4);
        sliderPop.setSnapToTicks(true);
        sliderPop.setValue(16);
        sliderPop.setValueIsAdjusting(false);
        settingsPanel.add(sliderPop, new GridConstraints(7, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        panelPop = new JPanel();
        panelPop.setLayout(new GridLayoutManager(1, 2, new Insets(0, 0, 0, 0), -1, -1));
        settingsPanel.add(panelPop, new GridConstraints(6, 0, 1, 1, GridConstraints.ANCHOR_SOUTH, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, false));
        labelPopPanel = new JLabel();
        this.$$$loadLabelText$$$(labelPopPanel, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.label.popPanel"));
        panelPop.add(labelPopPanel, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtPopSize = new JTextField();
        txtPopSize.setEditable(false);
        txtPopSize.setText("");
        panelPop.add(txtPopSize, new GridConstraints(0, 1, 1, 1, GridConstraints.ANCHOR_EAST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, new Dimension(50, 34), new Dimension(50, 34), null, 0, false));
        sliderParents = new JSlider();
        sliderParents.setMaximum(1000);
        sliderParents.setMinimum(2);
        sliderParents.setMinorTickSpacing(2);
        sliderParents.setSnapToTicks(true);
        sliderParents.setValue(8);
        settingsPanel.add(sliderParents, new GridConstraints(9, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        panelParents = new JPanel();
        panelParents.setLayout(new GridLayoutManager(1, 2, new Insets(0, 0, 0, 0), -1, -1));
        settingsPanel.add(panelParents, new GridConstraints(8, 0, 1, 1, GridConstraints.ANCHOR_SOUTH, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, false));
        labelParents = new JLabel();
        this.$$$loadLabelText$$$(labelParents, this.$$$getMessageFromBundle$$$("com/traveling/salesman/problem/res/Strings", "string.label.parent"));
        panelParents.add(labelParents, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtParentsAmount = new JTextField();
        txtParentsAmount.setEditable(false);
        txtParentsAmount.setText("");
        panelParents.add(txtParentsAmount, new GridConstraints(0, 1, 1, 1, GridConstraints.ANCHOR_EAST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, new Dimension(50, 34), new Dimension(50, 34), null, 0, false));
    }

    private static Method $$$cachedGetBundleMethod$$$ = null;

    private String $$$getMessageFromBundle$$$(String path, String key) {
        ResourceBundle bundle;
        try {
            Class<?> thisClass = this.getClass();
            if ($$$cachedGetBundleMethod$$$ == null) {
                Class<?> dynamicBundleClass = thisClass.getClassLoader().loadClass("com.intellij.DynamicBundle");
                $$$cachedGetBundleMethod$$$ = dynamicBundleClass.getMethod("getBundle", String.class, Class.class);
            }
            bundle = (ResourceBundle) $$$cachedGetBundleMethod$$$.invoke(null, path, thisClass);
        } catch (Exception e) {
            bundle = ResourceBundle.getBundle(path);
        }
        return bundle.getString(key);
    }

    /**
     * @noinspection ALL
     */
    private void $$$loadLabelText$$$(JLabel component, String text) {
        StringBuffer result = new StringBuffer();
        boolean haveMnemonic = false;
        char mnemonic = '\0';
        int mnemonicIndex = -1;
        for (int i = 0; i < text.length(); i++) {
            if (text.charAt(i) == '&') {
                i++;
                if (i == text.length()) break;
                if (!haveMnemonic && text.charAt(i) != '&') {
                    haveMnemonic = true;
                    mnemonic = text.charAt(i);
                    mnemonicIndex = result.length();
                }
            }
            result.append(text.charAt(i));
        }
        component.setText(result.toString());
        if (haveMnemonic) {
            component.setDisplayedMnemonic(mnemonic);
            component.setDisplayedMnemonicIndex(mnemonicIndex);
        }
    }

    /**
     * @noinspection ALL
     */
    private void $$$loadButtonText$$$(AbstractButton component, String text) {
        StringBuffer result = new StringBuffer();
        boolean haveMnemonic = false;
        char mnemonic = '\0';
        int mnemonicIndex = -1;
        for (int i = 0; i < text.length(); i++) {
            if (text.charAt(i) == '&') {
                i++;
                if (i == text.length()) break;
                if (!haveMnemonic && text.charAt(i) != '&') {
                    haveMnemonic = true;
                    mnemonic = text.charAt(i);
                    mnemonicIndex = result.length();
                }
            }
            result.append(text.charAt(i));
        }
        component.setText(result.toString());
        if (haveMnemonic) {
            component.setMnemonic(mnemonic);
            component.setDisplayedMnemonicIndex(mnemonicIndex);
        }
    }

    /**
     * @noinspection ALL
     */
    public JComponent $$$getRootComponent$$$() {
        return mainPanel;
    }

}