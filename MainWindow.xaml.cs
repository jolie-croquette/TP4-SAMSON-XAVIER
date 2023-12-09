using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TP4
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// !!!! NE PAS METTRE DE CODE ICI !!!! 

    public partial class MainWindow : Window
    {

        // #############################
        // #### VARIABLES GLOBALES #####
        // Ces variables sont accessible partout dans le code arrière.
        bool boutonCliquer = false;
        bool modifEnCours = false;
        string cheminNouvelleImage = null;
        int employerSelectionner = 0;

        /// <summary>
        ///  La matrice à utiliser pour récupérer les données extraites votre fichier .CSV
        /// </summary>
        public string[,] matrice;


        /// <summary>
        /// Chemin d'accès vers le fichier CSV à lire.
        /// </summary>
        string cheminAccesLecture = @"C:\data\420-04A-FX\TP4\TP4_SAMSON.csv";

        public MainWindow()
        {
            // NE PAS RETIRER CE CODE
            InitializeComponent();
            DatePckNaissance.Text = DateTime.Now.ToString();

            // Ici, c'est comme votre programme "Main" : vous pouvez coder essentiellement 
            // l'initialisation des vos variables, des champs par défaut de formulaire (si le cas),
            // Attention, vous n'avez pas à coder les appels des fonctions événements de l'interface graphique 
            // car celles-ci sont lancées par l'interration de l'utilisateur avec le programme (ex. click, sélection, etc)

        }

        /// <summary>
        /// Événement qui va essentiellement lancer la fonction LireCsvChargerMatrice avec le bon chemin d'accès, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnChargerFichier_Click(object sender, RoutedEventArgs e)
        {
            // CODE FOURNI
            // La fonction LireCsvChargerMatrice doit être complétée par vous.

            // Charger la liste déroulante de votre formulaire. Cette liste doit contenir les noms de vos entités
            // TODO
            if (!boutonCliquer)
            {
                string cheminAcces = @"C:\data\420-04A-FX\TP4\TP4_SAMSON.csv";
                matrice = LireCsvChargerMatrice(cheminAcces);
                cmbEmployer.Items.Clear();
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        cmbEmployer.Items.Add($"{i + 1}. {matrice[i, 1]}");
                    }
                }

                // Par défaut, la première entitée doit être sélectionnée dans la liste et son contenu doit être affiché dans le formulaire
                // TODO
                cmbEmployer.SelectedIndex = 0;

                MessageBox.Show("Donnée chargée", "EquiGuest", MessageBoxButton.OK, MessageBoxImage.Information);
                boutonCliquer = true;
            }
            else
            {
                MessageBox.Show("Donnée déjà chargé...", "EquiGuest", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        #region VOS FONCTIONS 

        //########################## VOS FONCTIONS ###############################
        //########################################################################
        //C'est à partir d'ici que vous coder votre propres fonctions, nécessaires pour votre TP.


        /// <summary>
        ///  Fonction responsable de lire le contenu d'un fichier CSV, de retirer la dernière ligne vide et de retourner
        ///  une matrice de strings où chaque case représente une ligne du fichier, EXCLUANT l'entête.
        /// </summary>
        /// <param name="cheminAcces">Chemin d'accès vers le fichier CSV</param>
        /// <returns>Une matrice contenant les lignes du fichier, EXCLUANT l'entête. </returns>
        static string[,] LireCsvChargerMatrice(string cheminAcces)
        {

            #region CODE FOURI
            // Ouvrir le fichier CSV
            StreamReader fichierEntree = new StreamReader(cheminAcces);

            // LIRE tout le fichier, INCLUANT la ligne d'entête
            string contenuFichier = fichierEntree.ReadToEnd();
            contenuFichier = contenuFichier.Replace("\r", "");
            string[] lignesFichierTemporaire = contenuFichier.Split('\n');
            string[] lignesFichier;

            // Si la dernière ligne est vide, on l'exclue dans le vecteur de lignes
            if (lignesFichierTemporaire[lignesFichierTemporaire.Length - 1] == "")
            {
                // On dimensionne le vecteur de ligne pour exclure  2 lignes (l'entête et la dernière ligne vide)
                lignesFichier = new string[lignesFichierTemporaire.Length - 2];
            }
            else
            {   // On dimensionne le vecteur de ligne pour exclure  1 ligne (l'entête)
                lignesFichier = new string[lignesFichierTemporaire.Length - 1];
            }

            // Remplir le vecteur de lignes avec les lignes du fichier, incluant l'entête.
            for (int i = 0; i < lignesFichier.Length; i++)
            {
                // [i+1] On commence à la ligne 1, car la ligne 0 est l'entête
                lignesFichier[i] = lignesFichierTemporaire[i + 1];
            }
            fichierEntree.Close();
            #endregion



            #region À COMPLÉTER

            /* ##### COMPLÉTER LE RESTE DE LA FONCTION ###### */
            /* ############################################## */

            // Trouver le nombre de lignes et de colonnes du fichier, afin de préparer
            // la construction de la matrice de strings
            // TODO

            int nbColonnes = lignesFichier[0].Split(';').Length;
            int nbLignes = lignesFichier.Length;

            // Création de la matrice : à modifier avec la bonne dimension
            // TODO
            string[,] employees = new string[nbLignes, nbColonnes];

            // Affectation de la matrice à partir des données de lignesFichier; 
            // TODO

            for (int i = 0; i < lignesFichier.Length; i++)
            {
                string[] vecteurProprieter = lignesFichier[i].Split(';');
                for (int j = 0; j < vecteurProprieter.Length; j++)
                {
                    employees[i, j] = vecteurProprieter[j];
                }
            }
            #endregion

            // Retourner une matrice
            return employees;

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbEmployer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employerSelectionner = cmbEmployer.SelectedIndex;
            CacherControleModification(sender, e);
            AfficherInfo(sender,e);
        }

        private void AfficherInfo(object sender, RoutedEventArgs e)
        {
            if (cmbEmployer.SelectedValue != null)
            {
                string selectedValue = cmbEmployer.SelectedValue.ToString();
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        if (selectedValue == $"{i + 1}. {matrice[i, 1]}".ToString())
                        {
                            string employerNom = selectedValue;

                            TxtBlockPrenom.Text = selectedValue.Substring(3);

                            TxtBlockNom.Text = matrice[i, 2].ToString();

                            TxtBlockID.Text = "Numéro d'employer : " + matrice[i, 0];

                            DatePckNaissance.Text = matrice[i, 9];

                            TxtBlockAge.Text = "(" + matrice[i, 3] + " ans)";
                            TxtBlockAge.Text = $"({CalculAge(DatePckNaissance.Text, i)} ans)";

                            TxtBoxSalaire.Text = FormatageText(matrice[i, 4], "argent");

                            TxtBoxPoste.Text = matrice[i, 5];

                            TxtBoxAnciennete.Text = matrice[i, 6];

                            string imagePath = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"));
                            imgEmployer.Source = new BitmapImage(new Uri(imagePath + "\\img\\" + matrice[i, 8], UriKind.Absolute));
                            imgEmployer.Source.Freeze();

                            TxtBoxCellulaire.Text = FormatageText(matrice[i, 10], "téléphone");

                            TxtBoxContactUrgence.Text = FormatageText(matrice[i, 11], "téléphone");

                            TxtBoxLienContactUrgence.Text = matrice[i, 12];

                            if (matrice[i, 7].ToLower() == "oui")
                            {
                                OptAugmentationOui.IsChecked = true;
                            }
                            else if (matrice[i, 7].ToLower() == "non")
                            {
                                OptAugmentationNon.IsChecked = true;
                            }

                        }
                    }
                }
            }
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (boutonCliquer && !modifEnCours)
            {
                AfficherControleModification(sender, e);
            }
            else if (modifEnCours)
            {
                MessageBoxResult result = MessageBox.Show("Quittez le mode de modification\n\nVous perdrez tous les changements apportés\n\nConfirmer...", "EquiGuest", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        CacherControleModification(sender, e);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Aucun employé sélectionné", "EquiGuest", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Êtes vous sure de vouloir annuler vos modification ?", "EquiGuest", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    AnnulerModification(sender, e);
                    CacherControleModification(sender, e);
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfficherControleModification(object sender, RoutedEventArgs e)
        {
            modifEnCours = true;
            StkSauvegarderQuitter.Visibility = Visibility.Hidden;

            TxtBoxSalaire.Text = matrice[employerSelectionner, 4];
            TxtBoxCellulaire.Text = FormatageText(TxtBoxCellulaire.Text, "modifTel");
            TxtBoxContactUrgence.Text = FormatageText(TxtBoxContactUrgence.Text, "modifTel");

            DatePckNaissance.IsEnabled = true;
            TxtBoxSalaire.IsEnabled = true;
            TxtBoxCellulaire.IsEnabled = true;
            TxtBoxLienContactUrgence.IsEnabled = true;
            TxtBoxContactUrgence.IsEnabled = true;
            TxtBoxPoste.IsEnabled = true;
            OptAugmentationOui.IsEnabled = true;
            OptAugmentationNon.IsEnabled = true;
            TxtBoxAnciennete.IsEnabled = true;

            DatePckNaissance.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
            TxtBoxSalaire.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
            TxtBoxCellulaire.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
            TxtBoxContactUrgence.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
            TxtBoxLienContactUrgence.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
            TxtBoxPoste.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
            StkOptAugmentation.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
            TxtBoxAnciennete.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };

            DatePckNaissance.BorderBrush = new SolidColorBrush(Colors.Yellow) { Opacity = 1 };

            StkSaveCancel.Visibility = Visibility.Visible;
            TxtBlockModification.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CacherControleModification(object sender, RoutedEventArgs e)
        {
            modifEnCours = false;
            StkSauvegarderQuitter.Visibility = Visibility.Visible;

            StkSaveCancel.Visibility = Visibility.Hidden;
            TxtBlockModification.Visibility = Visibility.Hidden;

            DatePckNaissance.IsEnabled = false;
            TxtBoxSalaire.IsEnabled = false;
            TxtBoxCellulaire.IsEnabled = false;
            TxtBoxLienContactUrgence.IsEnabled = false;
            TxtBoxContactUrgence.IsEnabled = false;
            TxtBoxPoste.IsEnabled = false;
            TxtBoxAnciennete.IsEnabled = false;
            OptAugmentationNon.IsEnabled = false;
            OptAugmentationOui.IsEnabled = false;

            DatePckNaissance.Background = null;
            TxtBoxSalaire.Background = null;
            TxtBoxCellulaire.Background = null;
            TxtBoxContactUrgence.Background = null;
            TxtBoxLienContactUrgence.Background = null;
            TxtBoxPoste.Background = null;
            StkOptAugmentation.Background = null;
            TxtBoxAnciennete.Background = null;

            DatePckNaissance.BorderBrush = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgEmployer_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (modifEnCours)
            {
                OpenFileDialog ImageEmployer = new OpenFileDialog();
                ImageEmployer.Filter = "Image files (*.jpg;*.png)|*.jpg; *.png|All Files (*.*)|*.*";
                ImageEmployer.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ImageEmployer.ShowDialog();

                if (ImageEmployer.ShowDialog() == true)
                {
                    cheminNouvelleImage = ImageEmployer.FileName;
                    imgEmployer.Source = new BitmapImage(new Uri(cheminNouvelleImage, UriKind.Absolute));
                    imgEmployer.Source.Freeze();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            BtnChargerFichier_Click(sender, e);
        }

        private void AnnulerModification(object sender, RoutedEventArgs e)
        {
            int ID = cmbEmployer.SelectedIndex;

            string employerNom = cmbEmployer.SelectedValue.ToString();
            TxtBlockPrenom.Text = cmbEmployer.SelectedValue.ToString().Substring(3);
            TxtBlockNom.Text = matrice[ID, 2].ToString();
            DatePckNaissance.Text = matrice[ID, 9];
            TxtBlockAge.Text = "(" + matrice[ID, 3] + " ans)";
            TxtBlockID.Text = "Numéro d'employer : " + matrice[ID, 0];
            TxtBoxCellulaire.Text = FormatageText(matrice[ID, 10], "téléphone");
            TxtBoxSalaire.Text = FormatageText(matrice[ID, 4], "argent");
            
            string imagePath = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"));
            imgEmployer.Source = new BitmapImage(new Uri(imagePath + "\\img\\" + matrice[ID, 8], UriKind.Absolute));
            imgEmployer.Source.Freeze();
        }

        private void ImgEmployer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (modifEnCours)
            {
                Cursor = Cursors.Hand;
            }
        }

        private void ImgEmployer_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                if (cmbEmployer.SelectedIndex == Convert.ToInt32(matrice[i, 0]))
                {
                    matrice[i, 3] = CalculAge(DatePckNaissance.Text, i);
                    matrice[i, 4] = TxtBoxSalaire.Text;
                    if (modifEnCours && !string.IsNullOrEmpty(cheminNouvelleImage))
                    {
                        string directory = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin")) + "img\\";
                        string cheminMatrice = cheminNouvelleImage;

                        // Déchargez l'élément Image
                        imgEmployer.Source = null;

                        // Copiez l'image vers la destination
                        if (!File.Exists(directory + Path.GetFileName(cheminMatrice)))
                        {
                            File.Copy(cheminNouvelleImage, Path.Combine(directory, Path.GetFileName(cheminNouvelleImage)), true);
                            matrice[i, 8] = Path.GetFileName(cheminNouvelleImage).ToString();
                        }

                    }
                    matrice[i, 9] = DatePckNaissance.Text;
                    matrice[i, 10] = TxtBoxCellulaire.Text;
                    Debug.WriteLine(DatePckNaissance.Text.Replace('-',' '));
                }
            }
            cmbEmployer.SelectedIndex++;
            cmbEmployer.SelectedIndex--;
        }

        public string CalculAge(string dateDeNaissance, int ID)
        {
            string[] split = dateDeNaissance.Split('-');
            int[] date = new int[split.Length];
            for (int i = 0; i < date.Length; i++)
            {
                date[i] = Convert.ToInt32(split[i]);
            }

            int age = DateTime.Now.Year - date[0];

            return Convert.ToString(age);
        }

        public static string FormatageText(string text, string type)
        {
            switch (type)
            {
                case "téléphone":
                    return String.Format("{0:(###) ###-####}", Convert.ToInt64(text));
                case "argent":
                    return String.Format("{0:C}", Convert.ToInt32(text));
                case "modifTel":
                    return text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                default:
                    return "null";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Quittez sans sauvegarder.\n\nVous perdrez toutes les modifications apportées\n\nConfirmer...", "EquiGuest", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Environment.Exit(0);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}