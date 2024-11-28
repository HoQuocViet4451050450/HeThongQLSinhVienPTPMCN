package Sprint2;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Test {

    public static void main(String[] args) {

        WebDriver driver = new ChromeDriver();

        try {
            // Điều hướng tới URL của trang
            driver.get("https://localhost:7117/SinhViens");

            // Tối đa hóa cửa sổ trình duyệt
            driver.manage().window().maximize();

            // Lấy danh sách GPA ban đầu từ bảng
            List<WebElement> gpaElements = driver.findElements(By.xpath("//table/tbody/tr/td[7]")); // Cột thứ 7 chứa GPA
            List<Double> originalGpaList = new ArrayList<>();
            for (WebElement gpaElement : gpaElements) {
                originalGpaList.add(Double.parseDouble(gpaElement.getText())); // Chuyển giá trị GPA từ String sang Double
            }
            System.out.println("Danh sách GPA ban đầu: " + originalGpaList);
            
         // Chờ 10 giây để quan sát
            Thread.sleep(10000);
            
            // Nhấn vào nút sắp xếp tăng dần GPA
            WebElement gpaAscButton = driver.findElement(By.xpath("//thead/tr[1]/th[7]/a[1]/i[1]"));
            gpaAscButton.click();

            // Chờ 10 giây để quan sát
            Thread.sleep(10000);

            // Lấy danh sách GPA sau khi sắp xếp tăng dần
            gpaElements = driver.findElements(By.xpath("//table/tbody/tr/td[7]"));
            List<Double> gpaListAfterAsc = new ArrayList<>();
            for (WebElement gpaElement : gpaElements) {
                gpaListAfterAsc.add(Double.parseDouble(gpaElement.getText()));
            }
            System.out.println("Danh sách GPA sau khi sắp xếp tăng dần: " + gpaListAfterAsc);

            // Kiểm tra sắp xếp tăng dần
            List<Double> sortedGpaList = new ArrayList<>(originalGpaList);
            Collections.sort(sortedGpaList);

            if (gpaListAfterAsc.equals(sortedGpaList)) {
                System.out.println("Danh sách GPA đã được sắp xếp tăng dần chính xác!");
            } else {
                System.out.println("Danh sách GPA không được sắp xếp tăng dần chính xác!");
            }

            // Nhấn vào nút sắp xếp giảm dần GPA
            WebElement gpaDescButton = driver.findElement(By.xpath("//thead/tr[1]/th[7]/a[1]/i[1]"));
            gpaDescButton.click();

            // Chờ 10 giây để quan sát
            Thread.sleep(10000);

            // Lấy danh sách GPA sau khi sắp xếp giảm dần
            gpaElements = driver.findElements(By.xpath("//table/tbody/tr/td[7]"));
            List<Double> gpaListAfterDesc = new ArrayList<>();
            for (WebElement gpaElement : gpaElements) {
                gpaListAfterDesc.add(Double.parseDouble(gpaElement.getText()));
            }
            System.out.println("Danh sách GPA sau khi sắp xếp giảm dần: " + gpaListAfterDesc);

            // Kiểm tra sắp xếp giảm dần
            sortedGpaList.sort(Collections.reverseOrder());

            if (gpaListAfterDesc.equals(sortedGpaList)) {
                System.out.println("Danh sách GPA đã được sắp xếp giảm dần chính xác!");
            } else {
                System.out.println("Danh sách GPA không được sắp xếp giảm dần chính xác!");
            }

        } catch (Exception e) {
            System.out.println("Đã xảy ra lỗi: " + e.getMessage());
        } finally {
            // Đóng trình duyệt
            driver.quit();
        }
    }
}
