using Microsoft.Win32;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                        string nomFamille = matrice[i, 2];
                        //string nomCouper = nomFamille.Substring(nomFamille.Length - 1);

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
            CacherControleModification(sender, e);
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (cmbEmployer.SelectedValue.ToString() == $"{i + 1}. {matrice[i, 1]}".ToString())
                    {
                        string employerNom = cmbEmployer.SelectedValue.ToString();
                        TxtBoxNomFamille.Text = cmbEmployer.SelectedValue.ToString().Substring(3);
                        TxtBoxNom.Text = matrice[i, 2].ToString();
                        TxtBlockID.Text = "Numéro d'employer : " + matrice[i, 0];
                        TxtBoxDateNaissance.Text = "Date de naissance : " + matrice[i, 9];
                        TxtBlockAge.Text = "(" + matrice[i, 3] + " ans)";
                        // Test with a hard-coded image path
                        string imagePath = @"C:\data\420-04A-FX\TP4\img\" + matrice[i, 8];
                        imgEmployer.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
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
            if (boutonCliquer)
            {
                AfficherControleModification(sender, e);
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
            StkSaveCancel.Visibility = Visibility.Visible;
            TxtBoxNom.IsEnabled = true;
            TxtBoxNomFamille.IsEnabled = true;
            TxtBoxDateNaissance.IsEnabled = true;
            TxtBlockModification.Visibility = Visibility.Visible;
            modifEnCours = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CacherControleModification(object sender, RoutedEventArgs e)
        {
            StkSaveCancel.Visibility = Visibility.Hidden;
            TxtBoxNom.IsEnabled = false;
            TxtBoxNomFamille.IsEnabled = false;
            TxtBoxDateNaissance.IsEnabled = false;
            TxtBlockModification.Visibility = Visibility.Hidden;
            modifEnCours = false;
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
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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
            TxtBoxNomFamille.Text = cmbEmployer.SelectedValue.ToString().Substring(3);
            TxtBoxNom.Text = matrice[ID, 2].ToString();
            TxtBoxDateNaissance.Text = matrice[ID, 9];
            TxtBlockAge.Text = "(" + matrice[ID, 3] + " ans)";
            TxtBlockID.Text = "Numéro d'employer : " + matrice[ID, 0];
            // Test with a hard-coded image path
            string imagePath = @"C:\data\420-04A-FX\TP4\img\" + matrice[ID, 8];
            imgEmployer.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
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
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    if (cmbEmployer.SelectedIndex == Convert.ToInt32(matrice[i, 0]))
                    {
                        string destinationFile = @"C:\data\420-04A-FX\TP4\img";
                        File.Copy(cheminNouvelleImage, Path.Combine(destinationFile, $"employer{matrice[i, 0] + 1}.jpg"), true);
                        matrice[i, 8] = Path.GetFileName(cheminNouvelleImage);
                    }
                }
            }
        }
    }
}